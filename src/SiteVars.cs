using System;
using System.Collections.Generic;
using System.Text;
using Landis.Core;
using Landis.Library.UniversalCohorts;
using Landis.SpatialModeling;

namespace Landis.Library.PnETCohorts
{
    public static class SiteVars
    {
        public static ISiteVar<SiteCohorts> SiteCohorts;
        public static ISiteVar<Library.UniversalCohorts.SiteCohorts> UniversalCohorts;
        public static ISiteVar<Pool> WoodyDebris;
        public static ISiteVar<Pool> Litter;
        public static ISiteVar<double> FineFuels;
        public static ISiteVar<float> PressureHead;
        public static ISiteVar<float> ExtremeMinTemp;
        public static ISiteVar<double> AnnualPE;
        public static ISiteVar<double> ClimaticWaterDeficit;
        public static ISiteVar<double> SmolderConsumption;
        public static ISiteVar<double> FlamingConsumption;
        public static ISiteVar<float[]> MonthlyPressureHead;
        public static ISiteVar<SortedList<float, float>[]> MonthlySoilTemp;
        public static ISiteVar<float> FieldCapacity;

        public static void Initialize()
        {
            SiteCohorts = Globals.ModelCore.Landscape.NewSiteVar<SiteCohorts>();
            UniversalCohorts = Globals.ModelCore.Landscape.NewSiteVar<Library.UniversalCohorts.SiteCohorts>();
            WoodyDebris = Globals.ModelCore.Landscape.NewSiteVar<Pool>();
            Litter = Globals.ModelCore.Landscape.NewSiteVar<Pool>();
            FineFuels = Globals.ModelCore.Landscape.NewSiteVar<double>();
            PressureHead = Globals.ModelCore.Landscape.NewSiteVar<float>();
            ExtremeMinTemp = Globals.ModelCore.Landscape.NewSiteVar<float>();
            AnnualPE = Globals.ModelCore.Landscape.NewSiteVar<double>();
            ClimaticWaterDeficit = Globals.ModelCore.Landscape.NewSiteVar<double>();
            SmolderConsumption = Globals.ModelCore.Landscape.NewSiteVar<double>();
            FlamingConsumption = Globals.ModelCore.Landscape.NewSiteVar<double>();
            MonthlyPressureHead = Globals.ModelCore.Landscape.NewSiteVar<float[]>();
            MonthlySoilTemp = Globals.ModelCore.Landscape.NewSiteVar<SortedList<float, float>[]>();
            FieldCapacity = Globals.ModelCore.Landscape.NewSiteVar<float>();

            Globals.ModelCore.RegisterSiteVar(WoodyDebris, "Succession.WoodyDebris");
            Globals.ModelCore.RegisterSiteVar(Litter, "Succession.Litter");
            Globals.ModelCore.RegisterSiteVar(FineFuels, "Succession.FineFuels");
            Globals.ModelCore.RegisterSiteVar(PressureHead, "Succession.PressureHead");
            Globals.ModelCore.RegisterSiteVar(ExtremeMinTemp, "Succession.ExtremeMinTemp");
            Globals.ModelCore.RegisterSiteVar(AnnualPE, "Succession.PET");
            Globals.ModelCore.RegisterSiteVar(ClimaticWaterDeficit, "Succession.CWD");
            Globals.ModelCore.RegisterSiteVar(SmolderConsumption, "Succession.SmolderConsumption");
            Globals.ModelCore.RegisterSiteVar(FlamingConsumption, "Succession.FlamingConsumption");
            Globals.ModelCore.RegisterSiteVar(MonthlyPressureHead, "Succession.MonthlyPressureHead");
            Globals.ModelCore.RegisterSiteVar(MonthlySoilTemp, "Succession.MonthlySoilTemp");
            Globals.ModelCore.RegisterSiteVar(FieldCapacity, "Succession.SoilFieldCapacity");
        }

        //MG20261102
        public static bool FindMatchingCohort(ActiveSite site, Library.UniversalCohorts.ICohort pnetCohort)
        {
            bool foundMatch = false;
            foreach (Library.UniversalCohorts.ISpeciesCohorts univSpeciesCohort in UniversalCohorts[site])
            {
                foreach (Library.UniversalCohorts.ICohort univCohort in univSpeciesCohort)
                {
                    if ((univCohort.Species == pnetCohort.Species)
                        && (univCohort.Data.Age == pnetCohort.Data.Age)
                        && (univCohort.Data.ANPP == pnetCohort.Data.ANPP))
                    {
                        foundMatch = true;
                        int delta = univCohort.Data.Biomass - pnetCohort.Data.Biomass;
                        if (delta > 0)
                        {
                            Globals.ModelCore.UI.WriteLine("   PnET SiteCohort for site {0}, species {1}, age {2}, PnET biomass {3} --> Univ biomass {4}.",
                                                           site.DataIndex, pnetCohort.Species.Name, pnetCohort.Data.Age, pnetCohort.Data.Biomass, univCohort.Data.Biomass);
                            pnetCohort.ChangeBiomass(delta);
                        }
                        return foundMatch;
                    }
                }
            }
            return foundMatch;
        }

        //MG20261102
        public static void UpdatePnETCohorts()
        {
            Globals.ModelCore.UI.WriteLine("Updating PnET SiteCohorts with UniversalCohorts data.");
            foreach (ActiveSite site in Globals.ModelCore.Landscape)
            {
                foreach (Library.UniversalCohorts.ISpeciesCohorts pnetSpeciesCohorts in SiteCohorts[site])
                {
                    foreach (Library.UniversalCohorts.ICohort pnetCohort in pnetSpeciesCohorts)
                    {
                        bool matchingCohort = FindMatchingCohort(site, pnetCohort);
                        if (!matchingCohort)
                        {
                            Globals.ModelCore.UI.WriteLine("   No matching UniversalCohort found for site {0}, species {1}, age {2} --> removing PnET SiteCohort",
                                                           site.DataIndex, pnetCohort.Species.Name, pnetCohort.Data.Age);
                            // PnETCohorts.SiteCohorts.RemoveCohort(pnetCohort, new ExtensionType(Names.ExtensionName+":external"));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Copy basic PnET Cohort information (Age, Biomass, ANPP)
        /// to *new* Universal Cohorts for use in other extensions.
        /// Called at the end of PlugIn.Initialize() and PlugIn.Run()
        /// steps. Creates new UniversalCohorts and overwrites whatever
        /// existed previously in that variable space.
        /// </summary>
        public static void UpdateUniversalCohorts()
        {
            Globals.ModelCore.UI.WriteLine("Updating UniversalCohorts with PnET SiteCohorts data.");
            foreach (ActiveSite site in Globals.ModelCore.Landscape)
            {
                UniversalCohorts[site] = new Library.UniversalCohorts.SiteCohorts();
                foreach (Library.UniversalCohorts.ISpeciesCohorts pnetSpeciesCohorts in SiteCohorts[site])
                {
                    foreach (Library.UniversalCohorts.ICohort pnetCohort in pnetSpeciesCohorts)
                        UniversalCohorts[site].AddNewCohort(pnetCohort.Species, pnetCohort.Data.Age,
                                                            pnetCohort.Data.Biomass, pnetCohort.Data.ANPP,
                                                            new System.Dynamic.ExpandoObject());
                }
                if (SiteCohorts[site] != null && UniversalCohorts[site] == null)
                    throw new Exception("Cannot convert PnET SiteCohorts to Universal Cohorts");
            }
        }
    }
}
