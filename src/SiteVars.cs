using System;
using System.Collections.Generic;
using System.Text;

using Landis.Core;
using Landis.Library.AgeOnlyCohorts;
using Landis.Library.Biomass;
using Landis.SpatialModeling;

namespace Landis.Library.PnETCohorts
{

    public static class SiteVars
    {
        public static ISiteVar<Pool> WoodyDebris;
        public static ISiteVar<Pool> Litter;
        public static ISiteVar<Double> FineFuels;
        public static ISiteVar<float> PressureHead;
        public static ISiteVar<float> ExtremeMinTemp;
        public static ISiteVar<double> AnnualPE;  //Annual Potential Evaporation
        public static ISiteVar<double> ClimaticWaterDeficit;
        public static ISiteVar<double> SmolderConsumption;
        public static ISiteVar<double> FlamingConsumption;
        public static ISiteVar<Landis.Library.PnETCohorts.SiteCohorts> SiteCohorts;
        public static ISiteVar<float[]> MonthlyPressureHead;
        public static ISiteVar<SortedList<float, float>[]> MonthlySoilTemp;
        public static ISiteVar<float> FieldCapacity;

        public static void Initialize()
        {
            WoodyDebris = Globals.ModelCore.Landscape.NewSiteVar<Pool>();
            Litter = Globals.ModelCore.Landscape.NewSiteVar<Pool>();
            FineFuels = Globals.ModelCore.Landscape.NewSiteVar<Double>();
            SiteCohorts = Globals.ModelCore.Landscape.NewSiteVar<SiteCohorts>();
            PressureHead = Globals.ModelCore.Landscape.NewSiteVar<float>();
            ExtremeMinTemp = Globals.ModelCore.Landscape.NewSiteVar<float>();
            AnnualPE = Globals.ModelCore.Landscape.NewSiteVar<Double>();
            ClimaticWaterDeficit = Globals.ModelCore.Landscape.NewSiteVar<Double>();
            SmolderConsumption = Globals.ModelCore.Landscape.NewSiteVar<Double>();
            FlamingConsumption = Globals.ModelCore.Landscape.NewSiteVar<Double>();
            MonthlyPressureHead = Globals.ModelCore.Landscape.NewSiteVar<float[]>();
            MonthlySoilTemp = Globals.ModelCore.Landscape.NewSiteVar<SortedList<float, float>[]>();
            FieldCapacity = Globals.ModelCore.Landscape.NewSiteVar<float>();

            Globals.ModelCore.RegisterSiteVar(WoodyDebris, "Succession.WoodyDebris");
            Globals.ModelCore.RegisterSiteVar(Litter, "Succession.Litter");
            Globals.ModelCore.RegisterSiteVar(FineFuels, "Succession.FineFuels");
            Globals.ModelCore.RegisterSiteVar(PressureHead, "Succession.PressureHead");
            Globals.ModelCore.RegisterSiteVar(ExtremeMinTemp, "Succession.ExtremeMinTemp");
            Globals.ModelCore.RegisterSiteVar(AnnualPE, "Succession.PET"); //FIXME
            Globals.ModelCore.RegisterSiteVar(ClimaticWaterDeficit, "Succession.CWD");
            Globals.ModelCore.RegisterSiteVar(SmolderConsumption, "Succession.SmolderConsumption");
            Globals.ModelCore.RegisterSiteVar(FlamingConsumption, "Succession.FlamingConsumption");
            Globals.ModelCore.RegisterSiteVar(MonthlyPressureHead, "Succession.MonthlyPressureHead");
            Globals.ModelCore.RegisterSiteVar(MonthlySoilTemp, "Succession.MonthlySoilTemp");
            Globals.ModelCore.RegisterSiteVar(FieldCapacity, "Succession.SoilFieldCapacity");
        }

    }
}
