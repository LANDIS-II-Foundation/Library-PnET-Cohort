using System;
using System.Collections.Generic;
using Landis.Core;
using Landis.Library.UniversalCohorts;
using Landis.SpatialModeling;

namespace Landis.Library.PnETCohorts
{
    /// <summary>
    /// Data for an individual cohort that is not shared with other cohorts.
    /// </summary>
    public struct CohortData
    {
        /// <summary>
        /// The cohort instance
        /// </summary>
        public Cohort Cohort;

        /// <summary>
        /// UniversalCohort data fields (Age, Biomass, and ANPP)
        /// </summary>
        public UniversalCohorts.CohortData UniversalData;

        /// <summary>
        /// The cohort's live aboveground biomass (g/m2).
        /// </summary>
        public float AGBiomass;

        /// <summary>
        /// The cohort's live total biomass (wood + root) (g/m2).
        /// </summary>
        public float TotalBiomass;

        /// <summary>
        /// Are trees phsyiologically active
        /// </summary>
        public bool Leaf_On;

        /// <summary>
        /// Max biomass achived in the cohorts' life time. 
        /// This value remains high after the cohort has reached its 
        /// peak biomass. It is used to determine canopy layers and
        /// prevents a cohort from descending in the canopy as it
        /// declines (g/m2)
        /// </summary>
        public float BiomassMax;

        /// <summary>
        /// Foliage (g/m2)
        /// </summary>
        public float Fol;

        /// <summary>
        /// Maximum Foliage For The Year (g/m2)
        /// </summary>
        public float MaxFolYear;

        /// <summary>
        /// Non-Soluble Carbons
        /// </summary>
        public float NSC;

        /// <summary>
        /// Defoliation Proportion
        /// </summary>
        public float DeFolProp;

        /// <summary>
        /// Annual Woody Senescence (g/m2)
        /// </summary>
        public float LastWoodySenescence;

        /// <summary>
        /// Annual Foliage Senescence (g/m2)
        /// </summary>
        public float LastFoliageSenescence;

        /// <summary>
        /// Last Average FRad
        /// </summary>
        public float LastFRad;

        /// <summary>
        /// Last Growing Season FRad
        /// </summary>
        public List<float> LastSeasonFRad;

        /// <summary>
        /// Adjusted Fraction of Foliage
        /// </summary>
        public float adjFracFol;

        /// <summary>
        /// Adjusted Half Sat
        /// </summary>
        public float AdjHalfSat;

        /// <summary>
        /// Adjusted Foliage Carbons
        /// </summary>
        public float adjFolN;

        /// <summary>
        /// Boolean whether cohort has been killed by cold temp relative to cold tolerance
        /// </summary>
        public int ColdKill;

        /// <summary>
        /// The Layer of the Cohort
        /// </summary>
        public byte Layer;

        /// <summary>
        /// Leaf area index per subcanopy layer (m/m)
        /// </summary>
        public float[] LAI;

        /// <summary>
        /// Leaf area index (m/m) maximum last year
        /// </summary>
        public float LastLAI;

        /// <summary>
        /// Aboveground Biomass last year
        /// </summary>
        public float LastAGBio;

        /// <summary>
        /// Gross photosynthesis (gC/mo)
        /// </summary>
        public float[] GrossPsn;

        /// <summary>
        /// Foliar respiration (gC/mo)
        /// </summary>
        public float[] FolResp;

        /// <summary>
        /// Net photosynthesis (gC/mo)
        /// </summary>
        public float[] NetPsn;

        /// <summary>
        /// Mainenance respiration (gC/mo)
        /// </summary>
        public float[] MaintenanceRespiration;

        /// <summary>
        /// Transpiration (mm/mo)
        /// </summary>
        public float[] Transpiration;

        /// <summary>
        /// PotentialTranspiration (mm/mo)
        /// </summary>
        public float[] PotentialTranspiration;

        /// <summary>
        /// Reduction factor for suboptimal radiation on growth
        /// </summary>
        public float[] FRad;

        /// <summary>
        /// Reduction factor for suboptimal or supra optimal water 
        /// </summary>
        public float[] FWater;

        /// <summary>
        /// Actual water used to calculate FWater
        /// </summary>
        public float[] Water;

        /// <summary>
        /// Actual pressurehead used to calculate FWater
        /// </summary>
        public float[] PressHead;

        /// <summary>
        /// Number of precip events allocated to sublayer
        /// </summary>
        public int[] NumEvents;

        /// <summary>
        /// Reduction factor for ozone 
        /// </summary>
        public float[] FOzone;

        /// <summary>
        /// Interception (mm/mo)
        /// </summary>
        public float[] Interception;

        /// <summary>
        /// Adjustment folN based on fRad
        /// </summary>
        public float[] AdjFolN;

        /// <summary>
        /// Adjustment fracFol based on fRad
        /// </summary>
        public float[] AdjFracFol;

        /// <summary>
        /// Modifier of CiCa ratio based on fWater and Ozone
        /// </summary>
        public float[] CiModifier;

        /// <summary>
        /// Adjustment to Amax based on CO2
        /// </summary>
        public float[] DelAmax;

        /// <summary>
        /// Proportion of layer biomass attributed to cohort
        /// </summary>
        public float BiomassLayerProp;

        /// <summary>
        /// Proportion of layer canopy (foliage) attributed to cohort
        /// </summary>
        public float CanopyLayerProp;

        /// <summary>
        /// Proportion of layer canopy growing space available to cohort
        /// </summary>
        public float CanopyGrowingSpace;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="cohort">
        /// The existing cohort instance we are extracting data from.
        public CohortData(Cohort cohort)
        {
            AdjFolN = cohort.AdjFolN;
            adjFolN = cohort.adjFolN;
            AdjFracFol = cohort.AdjFracFol;
            adjFracFol = cohort.adjFracFol;
            AdjHalfSat = cohort.AdjHalfSat;
            AGBiomass = (1 - cohort.SpeciesPnET.FracBelowG) * cohort.TotalBiomass + cohort.Fol;
            TotalBiomass = cohort.TotalBiomass;
            BiomassMax = cohort.BiomassMax;
            CiModifier = cohort.CiModifier;
            ColdKill = cohort.ColdKill;
            DeFolProp = cohort.DeFolProp;
            DelAmax = cohort.DelAmax;
            Fol = cohort.Fol;
            MaxFolYear = cohort.MaxFolYear;
            FolResp = cohort.FolResp;
            FOzone = cohort.FOzone;
            FRad = cohort.FRad;
            FWater = cohort.FWater;
            GrossPsn = cohort.GrossPsn;
            Interception = cohort.Interception;
            LAI = cohort.LAI;
            LastLAI = cohort.LastLAI;
            LastFoliageSenescence = cohort.LastFoliageSenescence;
            LastFRad = cohort.LastFRad;
            LastSeasonFRad = cohort.LastSeasonFRad;
            LastWoodySenescence = cohort.LastWoodySenescence;
            LastAGBio = cohort.LastAGBio;
            Layer = cohort.Layer;
            Leaf_On = cohort.Leaf_On;
            MaintenanceRespiration = cohort.MaintenanceRespiration;
            NetPsn = cohort.NetPsn;
            NSC = cohort.NSC;
            PressHead = cohort.PressHead;
            NumEvents = cohort.NumEvents;
            Transpiration = cohort.Transpiration;
            PotentialTranspiration = cohort.PotentialTranspiration;
            Water = cohort.Water;
            BiomassLayerProp = cohort.BiomassLayerProp;
            CanopyLayerProp = cohort.CanopyLayerProp;
            Cohort = cohort;
            CanopyGrowingSpace = cohort.CanopyGrowingSpace;

            UniversalData.Age = cohort.Age;
            UniversalData.Biomass = (int)(AGBiomass * cohort.CanopyLayerProp);
            UniversalData.ANPP = cohort.ANPP;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CohortData(ushort age, float totalBiomass, double totalANPP, ISpecies species, bool cohortStacking)
        {            
            AdjFolN = new float[Globals.IMAX];
            adjFolN = 0; ;
            AdjFracFol = new float[Globals.IMAX];
            adjFracFol = 0;
            AdjHalfSat = 0;
            ISpeciesPnET spc = SpeciesParameters.SpeciesPnET.AllSpecies[species.Index];
            Cohort = new Cohort(species, spc, 0, "", 1, cohortStacking);
            AGBiomass = (1 - spc.FracBelowG) * totalBiomass;
            TotalBiomass = totalBiomass;
            BiomassMax = totalBiomass;
            CiModifier = new float[Globals.IMAX];
            ColdKill = int.MaxValue;
            DeFolProp = 0;
            DelAmax = new float[Globals.IMAX];
            Fol = 0;
            MaxFolYear = 0;
            FolResp = new float[Globals.IMAX];
            FOzone = new float[Globals.IMAX];
            FRad = new float[Globals.IMAX];
            FWater = new float[Globals.IMAX];
            GrossPsn = new float[Globals.IMAX];
            Interception = new float[Globals.IMAX];
            LAI = new float[Globals.IMAX];
            LastFoliageSenescence = 0;
            LastFRad = 0;
            LastSeasonFRad = new List<float>();
            LastWoodySenescence = 0;
            LastAGBio = AGBiomass;
            Layer = 0;
            Leaf_On = false;
            MaintenanceRespiration = new float[Globals.IMAX];
            NetPsn = new float[Globals.IMAX];
            NSC = 0;
            PressHead = new float[Globals.IMAX];
            NumEvents = new int[Globals.IMAX];
            Transpiration = new float[Globals.IMAX];
            PotentialTranspiration = new float[Globals.IMAX];
            Water = new float[Globals.IMAX];
            BiomassLayerProp = 1.0f;
            float cohortLAI = 0;
            float cohortIdealFol = spc.FracFol * (float)Math.Exp(-spc.FrActWd * BiomassMax) * TotalBiomass;
            for (int i = 0; i < Globals.IMAX; i++)
              cohortLAI += Cohort.CalculateLAI(spc, cohortIdealFol, i, cohortLAI);
            LastLAI = cohortLAI;
            CanopyLayerProp = LastLAI / spc.MaxLAI;
            if (cohortStacking)
                CanopyLayerProp = 1.0f;
            CanopyGrowingSpace = 1.0f;

            UniversalData.Age = age;
            UniversalData.Biomass = (int)AGBiomass;
            UniversalData.ANPP = totalANPP;
        }
    }
}
