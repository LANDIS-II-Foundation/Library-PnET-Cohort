//  Authors:  Robert M. Scheller, James B. Domingo

using System;
using Landis.Library.UniversalCohorts;

namespace Landis.Library.PnETCohorts
{
    /// <summary>
    /// Methods for PnET cohorts.
    /// </summary>
    public static class Cohorts
    {
        private static int successionTimeStep;
        private static ICalculator biomassCalculator;

        /// <summary>
        /// The succession time step used by biomass cohorts.
        /// </summary>
        public static int SuccessionTimeStep
        {
            get 
            {
                return successionTimeStep;
            }
        }

        /// <summary>
        /// The calculator for computing how a cohort's biomass changes.
        /// </summary>
        public static ICalculator BiomassCalculator
        {
            get 
            {
                return biomassCalculator;
            }
        }

        //MG20260911 Doesn't appear to be called from anywhere else
        /// <summary>
        /// Initializes the cohorts module.
        /// </summary>
        /// <param name="successionTimeStep">
        /// The time step for the succession extension.  Unit: years
        /// </param>

        public static void Initialize(int successionTimeStep)
        {
            Cohorts.successionTimeStep = successionTimeStep;
        }

        //MG20260911 Is this an endlessly recursive function??? 
        //MG20260911 Good thing it doesn't appear to be called from anywhere else!
        /// <summary>
        /// Computes the total biomass for all the cohorts at a site.
        /// </summary>
        public static int ComputeBiomass(ISiteCohorts siteCohorts)
        {
            return ComputeBiomass(siteCohorts);
        }
        
        //MG20260911 Doesn't appear to be called from anywhere else
        /// <summary>
        /// Computes the total aboveground live biomass for all the cohorts at a site, and the
        /// total aboveground live biomass for all the young cohorts.
        /// </summary>
        public static int ComputeBiomass(ISiteCohorts siteCohorts,
                                         out int youngBiomass)
        {
            youngBiomass = 0;
            int totalBiomass = 0;
            foreach (ISpeciesCohorts speciesCohorts in (UniversalCohorts.ISiteCohorts)siteCohorts) {
                foreach (ICohort cohort in speciesCohorts)
                {
                    totalBiomass += (int)cohort.Data.UniversalData.Biomass;
                    if (cohort.Data.UniversalData.Age < successionTimeStep)
                        youngBiomass += (int)cohort.Data.UniversalData.Biomass;
                }
            }
            return totalBiomass;
        }

        //MG20260911 Doesn't appear to be called from anywhere else
        /// <summary>
        /// Computes the total aboveground live biomass for all the cohorts, not including young cohorts.
        /// </summary>
        public static int ComputeNonYoungBiomass(ISiteCohorts siteCohorts)
        {
            if (siteCohorts == null)
                return 0;
            int totalBiomass = 0;
            foreach (ISpeciesCohorts speciesCohorts in (UniversalCohorts.ISiteCohorts)siteCohorts)
                foreach (ICohort cohort in speciesCohorts)
                    if (cohort.Data.UniversalData.Age >= successionTimeStep)
                        totalBiomass += (int)cohort.Data.UniversalData.Biomass;
            return totalBiomass;
        }
    }
}
