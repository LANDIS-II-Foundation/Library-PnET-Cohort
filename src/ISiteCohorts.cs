using System.Collections.Generic;
using Landis.Core;
using Landis.Library.Parameters;
using Landis.Library.UniversalCohorts;

namespace Landis.Library.PnETCohorts 
{
    /// <summary>
    /// PnET SiteCohort interface
    /// Inherits from UniversalCohorts ISiteCohort interface
    /// </summary>
    public interface ISiteCohorts : UniversalCohorts.ISiteCohorts
    {
        float[] NetPsn { get; }
        float[] MaintResp{ get; }
        float[] GrossPsn{ get; }
        float[] FolResp { get; }
        float[] AverageAlbedo { get; }
        float[] ActiveLayerDepth { get; }
        float[] FrostDepth { get; }
        float[] MonthlyAvgSnowPack { get; }
        float[] MonthlyAvgWater { get; }
        float[] MonthlyAvgLAI { get; }
        float[] MonthlyEvap { get; }
        float[] MonthlyActualTrans { get; }
        float[] MonthlyLeakage { get; }
        float[] MonthlyInterception { get; }
        float[] MonthlyRunoff { get; }
        float[] MonthlyAET { get; }
        float[] MonthlyPotentialEvap { get; }
        float[] MonthlyPotentialTrans { get; }
        float CanopyLAImax{get;}
        float SiteMossDepth { get; }
        int AverageAge { get; }
        float BiomassSum { get; }
        float AbovegroundBiomassSum { get; }
        float WoodBiomassSum { get; }
        float WoodySenescenceSum { get; }
        float FoliageSenescenceSum { get; }
        int CohortCount { get; }
        float JulySubCanopyPar { get; }
        float SubCanopyParMAX { get; }
        double Litter{ get; }
        double WoodyDebris { get; }
        int AgeMax { get; }
        float WaterAvg { get; }
        float BelowGroundBiomassSum { get; }
        float FoliageSum { get; }
        float NSCSum { get; }
        float AETSum { get; } //mm
        float NetPsnSum { get; }
        float PET { get; }
        IEstablishmentProbability EstablishmentProbability { get; }
        Parameters.Species.AuxParm<int> CohortCountPerSpecies { get; }
        Parameters.Species.AuxParm<bool> SpeciesPresent { get; }
        Parameters.Species.AuxParm<int> MaxFoliageYearPerSpecies { get; }
        Parameters.Species.AuxParm<int> BiomassPerSpecies { get; }
        Parameters.Species.AuxParm<int> AbovegroundBiomassPerSpecies { get; }
        Parameters.Species.AuxParm<int> WoodBiomassPerSpecies { get; }
        Parameters.Species.AuxParm<int> BelowGroundBiomassPerSpecies { get; }
        Parameters.Species.AuxParm<int> FoliageBiomassPerSpecies { get; }
        Parameters.Species.AuxParm<int> NSCPerSpecies { get; }
        Parameters.Species.AuxParm<float> LAIPerSpecies { get; }
        Parameters.Species.AuxParm<int> WoodySenescencePerSpecies { get; }
        Parameters.Species.AuxParm<int> FoliageSenescencePerSpecies { get; }
        Parameters.Species.AuxParm<List<ushort>> CohortAges { get; }
        List<ISpecies> SpeciesByPlant { get; set; }
        List<ISpecies> SpeciesBySerotiny { get; set; }
        List<ISpecies> SpeciesByResprout { get; set; }
        List<ISpecies> SpeciesBySeed { get; set; }
        List<int> CohortsBySuccession { get; set; }
        List<int> CohortsByCold { get; set; }
        List<int> CohortsByHarvest { get; set; }
        List<int> CohortsByFire { get; set; }
        List<int> CohortsByWind { get; set; }
        List<int> CohortsByOther { get; set; }
    }
}
