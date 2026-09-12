using System.Collections.Generic;
using Landis.Library.UniversalCohorts;

namespace Landis.Library.PnETCohorts
{
    /// <summary>
    /// PnET Cohort interface
    /// Inherits from UniversalCohorts ICohort interface
    /// </summary>
    public interface ICohort : UniversalCohorts.ICohort
    {
        new CohortData Data { get; }
        int AGBiomass { get; }
        int TotalBiomass { get; }
        bool Leaf_On { get; }
        float BiomassMax { get; }
        float Fol { get; }
        float MaxFolYear { get; }
        float NSC { get; }
        float DeFolProp { get; }
        float LastWoodySenescence { get; }
        float LastFoliageSenescence { get; }
        float LastFRad { get; }
        List<float> LastSeasonFRad { get; }
        float adjFracFol { get; }
        float AdjHalfSat { get; }
        float adjFolN { get; }
        int ColdKill { get; }
        byte Layer { get; }
        float[] LAI { get; }
        float LastLAI { get; }
        float LastAGBio { get; }
        float[] GrossPsn { get; }
        float[] FolResp { get; }
        float[] NetPsn { get; }
        float[] MaintenanceRespiration { get; }
        float[] Transpiration { get; }
        float[] PotentialTranspiration { get; }
        float[] FRad { get; }
        float[] FWater { get; }
        float[] Water { get; }
        float[] PressHead { get; }
        int[] NumEvents { get; }
        float[] FOzone { get; }
        float[] Interception { get; }
        float[] AdjFolN { get; }
        float[] AdjFracFol { get; }
        float[] CiModifier { get; }
        float[] DelAmax { get; }
        float BiomassLayerProp { get; }
        float CanopyLayerProp { get; }
        float CanopyGrowingSpace { get; }
    }
}
