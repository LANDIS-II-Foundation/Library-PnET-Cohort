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
        public static ISiteVar<UniversalCohorts.SiteCohorts> UniversalCohorts;
        public static ISiteVar<Pool> WoodyDebris;
        public static ISiteVar<Pool> Litter;
        public static ISiteVar<double> FineFuels;
        public static ISiteVar<float> PressureHead;
        public static ISiteVar<float> ExtremeMinTemp;
        public static ISiteVar<double> AnnualPE;  //Annual Potential Evaporation
        public static ISiteVar<double> ClimaticWaterDeficit;
        public static ISiteVar<double> SmolderConsumption;
        public static ISiteVar<double> FlamingConsumption;
        public static ISiteVar<float[]> MonthlyPressureHead;
        public static ISiteVar<SortedList<float, float>[]> MonthlySoilTemp;
        public static ISiteVar<float> FieldCapacity;

        public static void Initialize()
        {
            SiteCohorts = Globals.ModelCore.Landscape.NewSiteVar<SiteCohorts>();
            UniversalCohorts = Globals.ModelCore.Landscape.NewSiteVar<UniversalCohorts.SiteCohorts>();
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

            Globals.ModelCore.RegisterSiteVar(SiteCohorts, "Succession.CohortsPnET");  //MG20260909 moved here from PlugIn.cs
            Globals.ModelCore.RegisterSiteVar(UniversalCohorts, "Succession.UniversalCohorts");  //MG20260908, 20260909 moved here from PlugIn.cs
            Globals.ModelCore.RegisterSiteVar(WoodyDebris, "Succession.WoodyDebris");
            Globals.ModelCore.RegisterSiteVar(Litter, "Succession.Litter");
            Globals.ModelCore.RegisterSiteVar(FineFuels, "Succession.FineFuels");
            Globals.ModelCore.RegisterSiteVar(PressureHead, "Succession.PressureHead");
            Globals.ModelCore.RegisterSiteVar(ExtremeMinTemp, "Succession.ExtremeMinTemp");
            Globals.ModelCore.RegisterSiteVar(AnnualPE, "Succession.PET"); //FIXME  //MG20260911 Fix what?
            Globals.ModelCore.RegisterSiteVar(ClimaticWaterDeficit, "Succession.CWD");
            Globals.ModelCore.RegisterSiteVar(SmolderConsumption, "Succession.SmolderConsumption");
            Globals.ModelCore.RegisterSiteVar(FlamingConsumption, "Succession.FlamingConsumption");
            Globals.ModelCore.RegisterSiteVar(MonthlyPressureHead, "Succession.MonthlyPressureHead");
            Globals.ModelCore.RegisterSiteVar(MonthlySoilTemp, "Succession.MonthlySoilTemp");
            Globals.ModelCore.RegisterSiteVar(FieldCapacity, "Succession.SoilFieldCapacity");
        }

        //MG20260911 moved here from PlugIn.cs and modified accordingly
        public static void UpdateUniversalCohorts()
        {
            Globals.ModelCore.UI.WriteLine("Updating UniversalCohorts with PnET SiteCohorts data.");
            foreach (ActiveSite site in Globals.ModelCore.Landscape)
            {
                UniversalCohorts[site] = new Library.UniversalCohorts.SiteCohorts();
                foreach(Library.UniversalCohorts.ISpeciesCohorts speciesCohort in SiteCohorts[site])
                {
                    foreach (Library.UniversalCohorts.ICohort cohort in speciesCohort)
                        UniversalCohorts[site].AddNewCohort(cohort.Species, cohort.Data.Age,
                                                            (int)cohort.Data.Biomass, cohort.Data.ANPP,
                                                            cohort.Data.AdditionalParameters);
                }
                if (SiteCohorts[site] != null && UniversalCohorts[site] == null)
                    throw new Exception("Cannot convert PnET SiteCohorts to Universal Cohorts");
            }
        }
    }
}
