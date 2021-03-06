using Landis.Core;
using Landis.SpatialModeling;
using System.Collections.Generic;

namespace Landis.Library.PnETCohorts
{
    /// <summary>
    /// Data for an individual cohort that is not shared with other cohorts.
    /// </summary>
    public struct CohortData
    {
        /// <summary>
        /// The cohort's age (years).
        /// </summary>
        public ushort Age;

        //---------------------------------------------------------------------

        /// <summary>
        /// The cohort's live aboveground biomass (g/m2).
        /// </summary>
        public float Biomass;
        //---------------------------------------------------------------------

        /// <summary>
        /// The cohort's live total biomass (wood + root) (g/m2).
        /// </summary>
        public float TotalBiomass;
        //---------------------------------------------------------------------

        /// <summary>
        /// Are trees phsyiologically active
        /// </summary>
        public bool Leaf_On;

        //---------------------------------------------------------------------

        /// <summary>
        /// Max biomass achived in the cohorts' life time. 
        /// This value remains high after the cohort has reached its 
        /// peak biomass. It is used to determine canopy layers where
        /// it prevents that a cohort could descent in the canopy when 
        /// it declines (g/m2)
        /// </summary>
        public float BiomassMax;

        //---------------------------------------------------------------------

        /// <summary>
        /// Foliage (g/m2)
        /// </summary>
        public float Fol;

        //---------------------------------------------------------------------

        /// <summary>
        /// Non-Soluble Carbons
        /// </summary>
        public float NSC;

        //---------------------------------------------------------------------

        /// <summary>
        /// Defoliation Proportion
        /// </summary>
        public float DeFolProp;

        //---------------------------------------------------------------------

        /// <summary>
        /// Annual Woody Senescence (g/m2)
        /// </summary>
        public float LastWoodySenescence;

        //---------------------------------------------------------------------

        /// <summary>
        /// Annual Foliage Senescence (g/m2)
        /// </summary>
        public float LastFoliageSenescence;

        //---------------------------------------------------------------------

        /// <summary>
        /// Last Average FRad
        /// </summary>
        public float LastFRad;

        //---------------------------------------------------------------------

        /// <summary>
        /// Last Growing Season FRad
        /// </summary>
        public List<float> LastSeasonFRad;

        //---------------------------------------------------------------------

        /// <summary>
        /// Adjusted Fraction of Foliage
        /// </summary>
        public float adjFracFol;

        //---------------------------------------------------------------------

        /// <summary>
        /// Adjusted Half Sat
        /// </summary>
        public float AdjHalfSat;

        //---------------------------------------------------------------------

        /// <summary>
        /// Adjusted Foliage Carbons
        /// </summary>
        public float adjFolN;

        //---------------------------------------------------------------------

        /// <summary>
        /// Boolean whether cohort has been killed by cold temp relative to cold tolerance
        /// </summary>
        public int ColdKill;

        //---------------------------------------------------------------------

        /// <summary>
        /// The Layer of the Cohort
        /// </summary>
        public byte Layer;

        //---------------------------------------------------------------------

        /// <summary>
        /// Leaf area index per subcanopy layer (m/m)
        /// </summary>
        public float[] LAI;

        //---------------------------------------------------------------------

        /// <summary>
        /// Gross photosynthesis (gC/mo)
        /// </summary>
        public float[] GrossPsn;

        //---------------------------------------------------------------------

        /// <summary>
        /// Foliar respiration (gC/mo)
        /// </summary>
        public float[] FolResp;

        //---------------------------------------------------------------------

        /// <summary>
        /// Net photosynthesis (gC/mo)
        /// </summary>
        public float[] NetPsn;

        //---------------------------------------------------------------------

        /// <summary>
        /// Mainenance respiration (gC/mo)
        /// </summary>
        public float[] MaintenanceRespiration;

        //---------------------------------------------------------------------

        /// <summary>
        /// Transpiration (mm/mo)
        /// </summary>
        public float[] Transpiration;

        //---------------------------------------------------------------------

        /// <summary>
        /// Reduction factor for suboptimal radiation on growth
        /// </summary>
        public float[] FRad;

        //---------------------------------------------------------------------

        /// <summary>
        /// Reduction factor for suboptimal or supra optimal water 
        /// </summary>
        public float[] FWater;

        //---------------------------------------------------------------------

        /// <summary>
        /// Actual water used to calculate FWater
        /// </summary>
        public float[] Water;

        //---------------------------------------------------------------------

        /// <summary>
        /// Actual pressurehead used to calculate FWater
        /// </summary>
        public float[] PressHead;

        //---------------------------------------------------------------------

        /// <summary>
        /// Reduction factor for ozone 
        /// </summary>
        public float[] FOzone;

        //---------------------------------------------------------------------

        /// <summary>
        /// Interception (mm/mo)
        /// </summary>
        public float[] Interception;

        //---------------------------------------------------------------------

        /// <summary>
        /// Adjustment folN based on fRad
        /// </summary>
        public float[] AdjFolN;

        //---------------------------------------------------------------------

        /// <summary>
        /// Adjustment fracFol based on fRad
        /// </summary>
        public float[] AdjFracFol;

        //---------------------------------------------------------------------

        /// <summary>
        /// Modifier of CiCa ratio based on fWater and Ozone
        /// </summary>
        public float[] CiModifier;

        //---------------------------------------------------------------------

        /// <summary>
        /// Adjustment to Amax based on CO2
        /// </summary>
        public float[] DelAmax;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="cohort">
        /// The cohort we are extracting data from.
        public CohortData(Cohort cohort)
        {
            this.AdjFolN = cohort.AdjFolN;
            this.adjFolN = cohort.adjFolN;
            this.AdjFracFol = cohort.AdjFracFol;
            this.adjFracFol = cohort.adjFracFol;
            this.AdjHalfSat = cohort.AdjHalfSat;
            this.Age = cohort.Age;
            this.Biomass = (1 - cohort.SpeciesPnET.FracBelowG) * cohort.TotalBiomass;
            this.TotalBiomass = cohort.TotalBiomass;
            this.BiomassMax = cohort.BiomassMax;
            this.CiModifier = cohort.CiModifier;
            this.ColdKill = cohort.ColdKill;
            this.DeFolProp = cohort.DefolProp;
            this.DelAmax = cohort.DelAmax;
            this.Fol = cohort.Fol;
            this.FolResp = cohort.FolResp;
            this.FOzone = cohort.FOzone;
            this.FRad = cohort.FRad;
            this.FWater = cohort.FWater;
            this.GrossPsn = cohort.GrossPsn;
            this.Interception = cohort.Interception;
            this.LAI = cohort.LAI;
            this.LastFoliageSenescence = cohort.LastFoliageSenescence;
            this.LastFRad = cohort.LastFRad;
            this.LastSeasonFRad = cohort.LastSeasonFRad;
            this.LastWoodySenescence = cohort.LastWoodySenescence;
            this.Layer = cohort.Layer;
            this.Leaf_On = cohort.Leaf_On;
            this.MaintenanceRespiration = cohort.MaintenanceRespiration;
            this.NetPsn = cohort.NetPsn;
            this.NSC = cohort.NSC;
            this.PressHead = cohort.PressHead;
            this.Transpiration = cohort.Transpiration;
            this.Water = cohort.Water;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="age">
        /// The age of the cohort.
        /// <param name="totalBioamss">
        /// The biomamss of the cohort
        public CohortData(ushort age, float totalBiomass, ISpecies species)
        {
            this.AdjFolN = new float[Globals.IMAX];
            this.adjFolN = 0; ;
            this.AdjFracFol = new float[Globals.IMAX];
            this.adjFracFol = 0;
            this.AdjHalfSat = 0;
            this.Age = age;
            ISpeciesPnET spc = SpeciesParameters.SpeciesPnET.AllSpecies[species.Index];
            this.Biomass = (1- spc.FracBelowG) * totalBiomass;
            this.TotalBiomass = totalBiomass;
            this.BiomassMax = totalBiomass;
            this.CiModifier = new float[Globals.IMAX];
            this.ColdKill = 0;
            this.DeFolProp = 0;
            this.DelAmax = new float[Globals.IMAX];
            this.Fol = 0;
            this.FolResp = new float[Globals.IMAX];
            this.FOzone = new float[Globals.IMAX];
            this.FRad = new float[Globals.IMAX];
            this.FWater = new float[Globals.IMAX];
            this.GrossPsn = new float[Globals.IMAX];
            this.Interception = new float[Globals.IMAX];
            this.LAI = new float[Globals.IMAX];
            this.LastFoliageSenescence = 0;
            this.LastFRad = 0;
            this.LastSeasonFRad = new List<float>();
            this.LastWoodySenescence = 0;
            this.Layer = 0;
            this.Leaf_On = false;
            this.MaintenanceRespiration = new float[Globals.IMAX];
            this.NetPsn = new float[Globals.IMAX];
            this.NSC = 0;
            this.PressHead = new float[Globals.IMAX];
            this.Transpiration = new float[Globals.IMAX];
            this.Water = new float[Globals.IMAX];
        }
    }
}
