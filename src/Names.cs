using System;
using System.Collections.Generic;

namespace Landis.Library.PnETCohorts
{
    public static class Names
    {
        public static SortedDictionary<string, Parameter<string>> parameters = new SortedDictionary<string, Parameter<string>>(StringComparer.InvariantCultureIgnoreCase);
        public const string ExtensionName = "PnET-Succession";
        public const string PNEToutputsites = "PNEToutputsites";
        public const string EcoregionParameters = "EcoregionParameters";
        public const string DisturbanceReductions = "DisturbanceReductions";
        public const string PnETGenericParameters = "PnETGenericParameters";
        public const string PnETGenericDefaultParameters = "PnETGenericDefaultParameters";
        public const string VanGenuchten = "VanGenuchten";
        public const string SaxtonAndRawls = "SaxtonAndRawlsParameters";
        public const string PnETSpeciesParameters = "PnETSpeciesParameters";
        public const string StartYear = "StartYear";
        public const string Timestep = "Timestep";
        public const string SeedingAlgorithm = "SeedingAlgorithm";
        //public const string MaxDevLyrAv = "MaxDevLyrAv";
        public const string LayerThreshRatio = "LayerThreshRatio";
        public const string MaxCanopyLayers = "MaxCanopyLayers";
        public const string IMAX = "IMAX";
        public const string InitialCommunities = "InitialCommunities";
        public const string InitialCommunitiesMap = "InitialCommunitiesMap";
        public const string InitialCommunitiesSpinup = "InitialCommunitiesSpinup";
        public const string MinFolRatioFactor = "MinFolRatioFactor";
        public const string LitterMap = "LitterMap";
        public const string WoodyDebrisMap = "WoodyDebrisMap";
        public const string ClimateConfigFile = "ClimateConfigFile";
        public const string MapCoordinates = "MapCoordinates";
        public const string PNEToutputSiteCoordinates = "PNEToutputSiteCoordinates";
        public const string PNEToutputSiteLocation = "PNEToutputSiteLocation";
        public const string PressureHeadCalculationMethod = "PressureHeadCalculationMethod";
        public const string Wythers = "Wythers";
        public const string DTemp = "DTemp";
        public const string CO2AMaxBEff = "CO2AMaxBEff";
        public const string SoilIceDepth = "SoilIceDepth";
        public const string LeakageFrostDepth = "LeakageFrostDepth";
        public const string CohortBinSize = "CohortBinSize";
        public const string InvertPest = "InvertPest";
        public const string PARunits = "PARunits";
        public const string SpinUpWaterStress = "SpinUpWaterStress";
        public const string PrecipEventsWithReplacement = "PrecipEventsWithReplacement";
        public const string Parallel = "Parallel";
        public const string CohortStacking = "CohortStacking";
        //public const string ETMethod = "ETMethod";
        public const string ETExtCoeff = "ETExtCoeff";
        public const string RETCropCoeff = "RETCropCoeff";
        public const string CanopySumScale = "CanopySumScale";
       

        //Ecoregion parameters
        public const string LeakageFrac = "LeakageFrac";
        public const string RunoffCapture = "RunoffCapture";
        public const string PrecLossFrac = "PrecLossFrac";
        public const string RootingDepth = "RootingDepth";
        public const string SoilType = "SoilType";
        public const string PrecIntConst = "PrecIntConst";
        public const string SnowSublimFrac = "SnowSublimFrac";
        public const string PrecipEvents = "PrecipEvents";
        public const string Latitude = "Latitude";
        public const string climateFileName = "climateFileName";
        public const string WinterSTD = "WinterSTD";
        public const string MossDepth = "MossDepth";
        public const string EvapDepth = "EvapDepth";
        public const string FrostFactor = "FrostFactor";

        //Species parameters
        public const string FolNShape = "FolNShape";
        public const string MaxFolN = "MaxFolN";
        public const string FracFolShape = "FracFolShape";
        public const string MaxFracFol = "MaxFracFol";
        public const string O3Coeff = "O3GrowthSens";
        public static readonly string[] MutuallyExclusiveCanopyTypes = new string[] { "dark", "light", "decid", "ground", "open" };
        public const string LeafOnMinT = "LeafOnMinT"; // Optional
        public const string RefoliationMinimumTrigger = "RefolMinimumTrigger";
        public const string RefoliationMaximum = "RefolMaximum";
        public const string RefoliationCost = "RefolCost";
        public const string NonRefoliationCost = "NonRefolCost";
        //---------------------------------------------------------------------
        // Does not appear this function is used anywhere
        public static void AssureIsName(string name)
        {
            if (IsName(name) == false)
            {
                string msg = name + " is not a keyword keywords are /n"+ string.Join("\n\t", AllNames.ToArray());
                throw new System.Exception(msg);
            }
        }
        //---------------------------------------------------------------------
        // Does not appear this function is used anywhere
        public static bool IsName(string name)
        {
            List<string> Names = AllNames;
            foreach (string _name in AllNames)
            {
                if (System.String.Compare(_name, name, System.StringComparison.OrdinalIgnoreCase) == 0) return true;
            }
            return false;
        }
        //---------------------------------------------------------------------
        public static List<string> AllNames
        {
            get
            {
                List<string> Names = new List<string>();
                foreach (var name in typeof(Names).GetFields())
                {
                    string value = name.GetValue(name).ToString();
                   
                    Names.Add(value);
                    //Console.WriteLine(value);
                }
                return Names;
            }
        }
        //---------------------------------------------------------------------
        public static void LoadParameters(SortedDictionary<string, Parameter<string>> modelParameters)
        {
            parameters = modelParameters;
        }
        //---------------------------------------------------------------------
        public static bool TryGetParameter(string label, out Parameter<string> parameter)
        {
            parameter = null;
            if (label == null)
            {
                return false;
            }

            if (parameters.ContainsKey(label) == false) return false;

            else
            {
                parameter = parameters[label];
                return true;
            }
        }
        //---------------------------------------------------------------------
        public static Dictionary<string, Parameter<string>> LoadTable(string label, List<string> RowLabels, List<string> Columnheaders, bool transposed = false)
        {
            string filename = GetParameter(label).Value;
            if (System.IO.File.Exists(filename) == false) throw new System.Exception("File not found " + filename);
            ParameterTableParser parser = new ParameterTableParser(filename, label, RowLabels, Columnheaders, transposed);
            Dictionary<string, Parameter<string>> parameters = Landis.Data.Load<Dictionary<string, Parameter<string>>>(filename, parser);
            return parameters;
        }
        //---------------------------------------------------------------------
        public static Parameter<string> GetParameter(string label)
        {
            if (parameters.ContainsKey(label) == false)
            {
                throw new System.Exception("No value provided for parameter " + label);
            }

            return parameters[label];

        }
        //---------------------------------------------------------------------
        public static Parameter<string> GetParameter(string label, float min, float max)
        {
            if (parameters.ContainsKey(label) == false)
            {
                throw new System.Exception("No value provided for parameter " + label);
            }

            Parameter<string> p = parameters[label];

            foreach (KeyValuePair<string, string> value in p)
            {
                float f;
                if (float.TryParse(value.Value, out f) == false)
                {
                    throw new System.Exception("Unable to parse value " + value.Value + " for parameter " + label + " unexpected format.");
                }
                if (f > max || f < min)
                {
                    throw new System.Exception("Parameter value " + value.Value + " for parameter " + label + " is out of range. [" + min + "," + max + "]");
                }
            }
            return p;
        }
        //---------------------------------------------------------------------
        public static bool HasMultipleMatches(string lifeForm, ref string[] matches)
        {
            int matchCount = 0;

            foreach (string type in Names.MutuallyExclusiveCanopyTypes)
            {
                if (!string.IsNullOrEmpty(lifeForm) && lifeForm.ToLower().Contains(type.ToLower()))
                {
                    matches[matchCount] = type;
                    matchCount += 1;
                }

                if (matchCount > 1)
                {
                    return true;
                }
            }

            return false;
        }
        //---------------------------------------------------------------------
    }
}
