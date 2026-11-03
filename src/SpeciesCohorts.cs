//  Authors:  Robert M. Scheller, James B. Domingo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Landis.Core;
using Landis.Library.UniversalCohorts;
using Landis.SpatialModeling;
using log4net;

namespace Landis.Library.PnETCohorts
{
    /// <summary>
    /// The cohorts for a particular species at a site.
    /// </summary>
    public class SpeciesCohorts : PnETCohorts.ISpeciesCohorts
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly bool isDebugEnabled = log.IsDebugEnabled;
        private ISpecies species;
        private bool isMaturePresent;
        private List<CohortData> cohortData;  // Cohort data is in oldest to youngest order.

        public int Count
        {
            get
            {
                return cohortData.Count;
            }
        }

        public Landis.Core.ISpecies Species
        {
            get 
            {
                int sppIndex = species.Index;
                return (Landis.Core.ISpecies)Globals.ModelCore.Species[sppIndex];
            }
        }

        public bool IsMaturePresent
        {
            get
            {
                return isMaturePresent;
            }
        }

        public ICohort this[int index]
        {
            get
            {
                return new Cohort(species, cohortData[index]);
            }
        }

        /// <summary>
        /// An iterator from the oldest cohort to the youngest.
        /// </summary>
        public OldToYoungIterator OldToYoung
        {
            get
            {
                return new OldToYoungIterator(this);
            }
        }

        /// <summary>
        /// Initializes a new instance with one young cohort (age = 1).
        /// </summary>
        public SpeciesCohorts(Cohort cohort)
        {
            this.species = cohort.Species;
            this.cohortData = new List<CohortData>();
            this.isMaturePresent = false;
            AddNewCohort(cohort);
        }

        /// <summary>
        /// Creates a copy of a species' cohorts.
        /// </summary>
        public SpeciesCohorts Clone()
        {
            SpeciesCohorts clone = new SpeciesCohorts(this.species);
            clone.cohortData = new List<CohortData>(this.cohortData);
            clone.isMaturePresent = this.isMaturePresent;
            return clone;
        }

        /// <summary>
        /// Initializes a new instance with no cohorts.
        /// Private constructor used by Clone method.
        /// </summary>
        private SpeciesCohorts(ISpecies species)
        {
            this.species = species;
        }

        /// <summary>
        /// Adds a new cohort.
        /// </summary>
        public void AddNewCohort(Cohort c)
        {
            this.cohortData.Add(new CohortData(c));
        }

        /// <summary>
        /// Gets the age of a cohort at a specific index.
        /// </summary>
        public int GetAge(int index)
        {
            return cohortData[index].UniversalData.Age;
        }

        /// <summary>
        /// Combines all young cohorts into a single cohort whose age is the
        /// succession timestep - 1 and whose biomass is the sum of all the
        /// biomasses of the young cohorts.
        /// </summary>
        /// <remarks>
        /// The age of the combined cohort is set to the succession timestep -
        /// 1 so that when the combined cohort undergoes annual growth, its
        /// age will end up at the succession timestep.
        /// <p>
        /// For this method, young cohorts are those whose age is less than or
        /// equal to the succession timestep.  We include the cohort whose age
        /// is equal to the timestep because such a cohort is generated when
        /// reproduction occurs during a succession timestep.
        /// </remarks>
        public void CombineYoungCohorts()
        {
            //  Work from the end of cohort data since the array is in old-to-
            //  young order.
            int youngCount = 0;
            float totalBiomass = 0;
            double totalANPP = 0;
            for (int i = cohortData.Count - 1; i >= 0; i--)
            {
                CohortData data = cohortData[i];
                if (data.UniversalData.Age <= Cohorts.SuccessionTimeStep)
                {
                    youngCount++;
                    totalBiomass += data.TotalBiomass;
                    totalANPP += data.UniversalData.ANPP;
                }
                else
                    break;
            }
            if (youngCount > 0)
            {
                cohortData.RemoveRange(cohortData.Count - youngCount, youngCount);
                bool cohortStacking = ((Parameter<bool>)Names.GetParameter(Names.CohortStacking)).Value;
                cohortData.Add(new CohortData((ushort)(Cohorts.SuccessionTimeStep - 1), totalBiomass, totalANPP, this.Species, cohortStacking));
            }
        }

        /// <summary>
        /// Grows an individual cohort for a year, incrementing its age by 1
        /// and updating its biomass for annual growth and mortality.
        /// </summary>
        /// <param name="index">
        /// The index of the cohort to grow; it must be between 0 and Count - 1.
        /// </param>
        /// <param name="site">
        /// The site where the species' cohorts are located.
        /// </param>
        /// <returns>
        /// The index of the next younger cohort.  Note this may be the same
        /// as the index passed in if that cohort dies due to senescence.
        /// </returns>
        public int GrowCohort(int index, ActiveSite site)
        {
            Debug.Assert(0 <= index && index <= cohortData.Count);
            Debug.Assert(site != null);
            Cohort cohort = new Cohort(species, cohortData[index]);
            if (isDebugEnabled)
                log.DebugFormat("  grow cohort: {0}, {1} yrs, {2} Mg/ha",
                                cohort.Species.Name, cohort.Age, cohort.TotalBiomass);
            //  Check for senescence
            if (cohort.Age >= species.Longevity)
            {
                RemoveCohort(index, cohort, site, null);
                return index;
            }
            cohort.IncrementAge();
            int biomassChange = (int)Cohorts.BiomassCalculator.ComputeChange(cohort, site); //, siteBiomass, prevYearSiteMortality);
            Debug.Assert(-cohort.TotalBiomass <= biomassChange);  // Cohort can't lose more biomass than it has
            cohort.ChangeBiomass(biomassChange);
            if (cohort.TotalBiomass > 0)
            {
                cohortData[index] = cohort.Data;
                return index + 1;
            }
            else
            {
                RemoveCohort(index, cohort, site, null);
                return index;
            }
        }

        private void RemoveCohort(int index, ICohort cohort,
                                  ActiveSite site, ExtensionType disturbanceType)
        {
            if (isDebugEnabled)
                log.DebugFormat("  cohort removed: {0}, {1} yrs, {2} g/m2 ({3})",
                                cohort.Species.Name, cohort.Data.UniversalData.Age, cohort.Data.UniversalData.Biomass,
                                disturbanceType != null
                                    ? disturbanceType.Name
                                    : cohort.Data.UniversalData.Age >= species.Longevity
                                        ? "senescence"
                                        : cohort.Data.UniversalData.Biomass == 0
                                            ? "attrition"
                                            : "UNKNOWN");
            cohortData.RemoveAt(index);
            Cohort.Died(this, cohort, site, disturbanceType);
        }

        private void ReduceCohort(ICohort cohort,
                                  ActiveSite site,
                                  ExtensionType disturbanceType, float reduction)
        {
            Cohort.PartialMortality(this, cohort, site, disturbanceType, reduction);
        }

        /// <summary>
        /// Updates the IsMaturePresent property.
        /// </summary>
        /// <remarks>
        /// Should be called after all the species' cohorts have grown.
        /// </remarks>
        public void UpdateMaturePresent()
        {
            isMaturePresent = false;
            for (int i = 0; i < cohortData.Count; i++) 
            {
                if (cohortData[i].UniversalData.Age >= species.Maturity) 
                {
                    isMaturePresent = true;
                    break;
                }
            }
        }
        
        /// <summary>
        /// Computes how much a disturbance damages the cohorts by reducing
        /// their biomass.
        /// </summary>
        /// <returns>
        /// The total of all the cohorts' biomass reductions.
        /// </returns>
        public int MarkCohorts(IDisturbance disturbance)
        {
            //  Go backwards through list of cohort data, so the removal of an
            //  item doesn't mess up the loop.
            isMaturePresent = false;
            int totalReduction = 0;
            for (int i = cohortData.Count - 1; i >= 0; i--)
            {
                Cohort cohort = new Cohort(species, cohortData[i]);
                double reduction = disturbance.ReduceOrKillMarkedCohort(cohort);
                int intReduction = (int)reduction;
                if (reduction > 0)
                {
                    totalReduction += intReduction;
                    if (reduction < cohort.Biomass)
                    {
                        ReduceCohort(cohort, disturbance.CurrentSite, disturbance.Type, intReduction);
                        cohort.ChangeBiomass(-intReduction);
                        cohortData[i] = cohort.Data;
                    }
                    else
                    {
                        RemoveCohort(i, cohort, disturbance.CurrentSite, disturbance.Type);
                        cohort = null;
                    }
                }
                if (cohort != null && cohort.Age >= species.Maturity)
                    isMaturePresent = true;
            }
            return totalReduction;
        }

        private static SpeciesCohortBoolArray isSpeciesCohortDamaged;

        static SpeciesCohorts()
        {
            isSpeciesCohortDamaged = new SpeciesCohortBoolArray();
        }

        /// <summary>
        /// Removes the cohorts that are completely removed by disturbance.
        /// </summary>
        /// <returns>
        /// The total biomass of all the cohorts damaged by the disturbance.
        /// </returns>
        public int MarkCohorts(ISpeciesCohortsDisturbance disturbance)
        {
            isSpeciesCohortDamaged.SetAllFalse(Count);
            disturbance.MarkCohortsForDeath(this, isSpeciesCohortDamaged);
            //  Go backwards through list of cohort data, so the removal of an
            //  item doesn't mess up the loop.
            isMaturePresent = false;
            int totalReduction = 0;
            for (int i = cohortData.Count - 1; i >= 0; i--)
            {
                if (isSpeciesCohortDamaged[i])
                {
                    Cohort cohort = new Cohort(species, cohortData[i]);
                    totalReduction += cohort.Biomass;
                    RemoveCohort(i, cohort, disturbance.CurrentSite, disturbance.Type);
                    Cohort.KilledByAgeOnlyDisturbance(this, cohort, disturbance.CurrentSite, disturbance.Type);
                    cohort = null;
                }
                else if (cohortData[i].UniversalData.Age >= species.Maturity)
                    isMaturePresent = true;
            }
            return totalReduction;
        }

        IEnumerator<PnETCohorts.ICohort> GetEnumerator()
        {
            foreach (CohortData data in cohortData)
                yield return new Cohort(species, data);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ICohort>)this).GetEnumerator();
        }

        IEnumerator<Library.UniversalCohorts.ICohort> IEnumerable<Library.UniversalCohorts.ICohort>.GetEnumerator()
        {
            foreach (CohortData data in cohortData)
                yield return new Library.UniversalCohorts.Cohort(species, data.UniversalData.Age, data.UniversalData.Biomass, new System.Dynamic.ExpandoObject());
        }
    }
}
