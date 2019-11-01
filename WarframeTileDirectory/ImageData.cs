using System;
using System.Collections.Generic;
using System.Linq;

namespace WarframeTileDirectory
{
    public class ImageData
    {
        public string Mission { get; set; }
        public string Tileset { get; set; }
        public string TileName { get; set; }
        public string MissionString { get; set; }
        public string Log { get; set; }
        public string Coordinates { get; set; }
        public string Faction { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }



        private string metaData;
        private string tilesetPlusMissionFromMetaData;
        private string rawTileNameFromMetaData;

        public ImageData(string metaData)
        {
            this.metaData = metaData;
            SetMetaData();
            this.Mission = GetMissionFromMetaData();
            this.Tileset = GetTilesetFromMetaData();
            this.TileName = AdjustTileNameForUniqunenessBetweenTilesets();
        }

        private void SetMetaData()
        {
            /* The Metadata Comment in Warframe Screenshot JPGs looks like this:
            * 
            * /Lotus/Levels/Proc/Corpus/CorpusShipExterminate/EKEdCEI+QQEQ6hUlxfQ9M5zMyAgYRFgAQA0ACAEA.lp Zone: /Lotus/Levels/Guild/CargoElevator2  P: -56, 5.0, -61  H:148  Log: 2975.081
            * 
            */
            string[] metaDataSplit = metaData.Split(' ');
            string[] firstSectionOfMetaData = metaDataSplit[0].Split('/');
            string[] secondSectionOfMetaData = metaDataSplit[3].Split('/');


            this.tilesetPlusMissionFromMetaData = firstSectionOfMetaData[5];
            this.tilesetPlusMissionFromMetaData = DealWithSpecialCaseTilesetNames(this.tilesetPlusMissionFromMetaData);

            this.rawTileNameFromMetaData = secondSectionOfMetaData[4];
        }


        private string DealWithSpecialCaseTilesetNames(string stringTilesetMission)
        {
            if (stringTilesetMission.Contains("TR")) // special case, just get it over with.
            {
                return "CorpusArchwing";
            }
            else if (stringTilesetMission.Contains("Space"))
            {
                return "GrineerArchwing";
            }
            else if (stringTilesetMission == "CorpusGasBoss")
            {
                return "CorpusGasCity";
            }
            else
            {
                return stringTilesetMission;
            }
        }

        private string GetTilesetFromMetaData()
        {
            return TilesetFromMission(tilesetPlusMissionFromMetaData);
        }

        private string TilesetFromMission(string value)
        {
            string[] tilesetArray = TileLogic.GenerateTilesetList();

            string name = tilesetArray.Where(x => value.Contains(x)).First().ToString();

            if (string.IsNullOrEmpty(name))
            {
                return value + "??? - Check Me: ";
            }
            else
            {
                return name;
            }
        }


        private string GetMissionFromMetaData()
        {

            return DetermineMissionTypeWithSpecialCases(tilesetPlusMissionFromMetaData);

        }

        public string DetermineMissionTypeWithSpecialCases(string value)
        {

            var types = TileLogic.GenerateMissionList();

            string name = types.Where(x => value.Contains(x.InGameName)).First().CommonName;

            //Special case consideration
            if (value == "CorpusShip")
            {
                return "Assassination (The Sergeant)";

            }
            else if (value == "GrineerAsteroid")
            {
                return "Sabotage (Drill)";

            }
            else if (string.IsNullOrEmpty(name))
            {
                return value + "??? - Check Me: ";

            }
            else
            {
                return name;
            }

        }


        private string AdjustTileNameForUniqunenessBetweenTilesets()
        {

            if (this.Tileset == "CorpusGasCity")
            {
                return GasCityTileNameFix();
            }
            else if (this.Tileset == "CorpusArchwing")
            {
                return CropusArchwingTileNameFix();
            }
            else if (this.Tileset == "CorpusIcePlanet")
            {
                return IcePlanetTileNameFix();
            }
            else if (this.Tileset == "CorpusOutpost")
            {
                return CorpusOutpustTileNameFix();
            }
            else if (this.Tileset == "CorpusShip")
            {
                return CorpusShipTileNameFix();
            }
            else if (this.Tileset == "CorpusToGrineer")
            {
                return C2GInvasionTileNameFix();
            }
            else if (this.Tileset == "GrineerArchwing")
            {
                return "GrineerArchwing" + rawTileNameFromMetaData.Replace("Spline", "");
            }
            else if (this.Tileset == "GrineerAsteroid")
            {
                return GrnAsteroidTileNameFix();
            }
            else if (this.Tileset == "GrineerForest")
            {
                return GrnForestTileNameFix();
            }
            else if (this.Tileset == "GrineerFortress")
            {
                return KuvaFortressTileNameFix();
            }
            else if (this.Tileset == "GrineerGalleon")
            {
                return GrnGalleonTileNameFix();
            }
            else if (this.Tileset == "GrineerOcean")
            {
                return GrnSeaLabTileNameFix();
            }
            else if (this.Tileset == "GrineerSettlement")
            {
                return MarsTileNameFix();

            }
            else if (this.Tileset == "GrineerShipyards")
            {
                return GrnShipyardsTileNameFix();
            }
            else if (this.Tileset == "GrineerToCorpus")
            {
                return G2CInvasionTileNameFix();
            }

            else if (this.Tileset == "InfestedCorpusShip")
            {
                return InfestedShipTileNameFix();
            }

            else if (this.Tileset == "OrokinMoon")
            {
                return LuaTileNameFix();
            }
            else if (this.Tileset == "OrokinTower")
            {
                return OrokinTowerTileNameFix();
            }
            else if (this.Tileset == "OrokinTowerDerelict")
            {
                return DerelictTileNameFix();

            }

            return "??? tset: " + this.Tileset + "tile: " + this.rawTileNameFromMetaData;

        }

        private string DerelictTileNameFix()
        {
            return "OrokinDerelict" + rawTileNameFromMetaData.Replace("OrokinTowerDerelict", "")
                                            .Replace("OrokinTower", "")
                                            .Replace("OrokinDerelict", "")
                                            .Replace("TowerDerelict", "")
                                            .Replace("Orokin", "")
                                            .Replace("Tower", "")
                                            .Replace("Derelict", "")
                                            .Replace("Oro", "");

        }

        private string OrokinTowerTileNameFix()
        {
            return "OrokinTower" + rawTileNameFromMetaData.Replace("OrokinTower", "")
                                            .Replace("Orokin", "")
                                            .Replace("Tower", "")
                                            .Replace("Oro", "");
            
        }

        private string LuaTileNameFix()
        {
            return "OrokinMoon" + rawTileNameFromMetaData.Replace("OrokinMoon", "")
                                        .Replace("OrokinLua", "")
                                        .Replace("Orokin", "")
                                        .Replace("Lua", "")
                                        .Replace("Moon", "")
                                        .Replace("Oro", "");
            
        }

        private string InfestedShipTileNameFix()
        {
            return "Infested" + rawTileNameFromMetaData.Replace("InfestedCorpusShip", "")
                                        .Replace("CorpusShip", "")
                                        .Replace("Infested", "");
             
        }

        private string GrnShipyardsTileNameFix()
        {
            return "Grineer" + rawTileNameFromMetaData.Replace("GrineerShipyards", "")
                                        .Replace("GrnShipyards", "")
                                        .Replace("Shipyards", "")
                                        .Replace("Grineer", "")
                                        .Replace("Grn", "");
            
        }

        private string MarsTileNameFix()
        {
            return "GrineerSettlement" + rawTileNameFromMetaData.Replace("GrineerSettlement", "")
                                                .Replace("GrineerCamp", "")
                                                .Replace("GrinnerCmp", "")
                                                .Replace("GrnSettlement", "")
                                                .Replace("GrnCamp", "")
                                                .Replace("GrnCmp", "")
                                                .Replace("Settlment", "")
                                                .Replace("Camp", "")
                                                .Replace("Grineer", "")
                                                .Replace("Grn", "")
                                                .Replace("Cmp", "");
            
        }

        private string GrnSeaLabTileNameFix()
        {
            return "GrineerOcean" +  rawTileNameFromMetaData.Replace("GrineerOcean", "")
                                            .Replace("GrnOcean", "")
                                            .Replace("Ocean", "")
                                            .Replace("Grineer", "")
                                            .Replace("Grn", "");
            
        }

        private string GrnGalleonTileNameFix()
        {
            return "GrineerGalleon" + rawTileNameFromMetaData.Replace("GrineerGalleon", "")
                                            .Replace("GrnGalleon", "")
                                            .Replace("Grineer", "")
                                            .Replace("Galleon", "")
                                            .Replace("Grn", "");
             
        }

        private string KuvaFortressTileNameFix()
        {
            return "GrineerFortress" + rawTileNameFromMetaData.Replace("GrineerFortress", "")
                                            .Replace("GrineerFort", "")
                                            .Replace("GrnFortress", "")
                                            .Replace("GrnFort", "")
                                            .Replace("Fortress", "")
                                            .Replace("Fort", "")
                                            .Replace("Grn", "");
             
        }

        private string GrnForestTileNameFix()
        {
            return "GrineerForest" + rawTileNameFromMetaData.Replace("GftRemastered", "")
                                            .Replace("GrineerForest", "")
                                            .Replace("GrnForest", "")
                                            .Replace("Forest", "")
                                            .Replace("Grn", "")
                                            .Replace("Gft", "");
             
        }

        private string GrnAsteroidTileNameFix()
        {
            return "GrineerAsteroid" + rawTileNameFromMetaData.Replace("GrineerAsteroid", "")
                                            .Replace("GrnAsteroid", "")
                                            .Replace("Asteroid", "")
                                            .Replace("Grn", "");
             
        }

        private string C2GInvasionTileNameFix()
        {
            //Seems counterintuitive - but this way we can just pass any C2G invasion tile to it and even if it has already been fixed it will pass.
            return "InvasionC2G" + rawTileNameFromMetaData.Replace("InvasionC2G", "");
        }
        private string G2CInvasionTileNameFix()
        {
            return "InvasionG2C" + rawTileNameFromMetaData.Replace("InvasionG2C", "");
        }

        private string CorpusShipTileNameFix()
        {
            return "CorpusShip" + rawTileNameFromMetaData.Replace("GuildShip", "")
                                        .Replace("CorpusShip", "")
                                        .Replace("CrpShip", "")
                                        .Replace("Corpus", "")
                                        .Replace("Crp", "")
                                        .Replace("Ship", "")
                                        .Replace("Guild", "");
            
        }

        private string CorpusOutpustTileNameFix()
        {
            return "CorpusOutpost" + rawTileNameFromMetaData.Replace("CorpusOutpost", "")
                                        .Replace("CrpOutpost", "")
                                        .Replace("Outpost", "")
                                        .Replace("Crp", "");
            
        }

        private string IcePlanetTileNameFix()
        {
            return "CorpusIce" + rawTileNameFromMetaData.Replace("CorpusIcePLanet", "")
                                        .Replace("IcePlanet", "")
                                        .Replace("CrpIce", "")
                                        .Replace("Ice", "")
                                        .Replace("Crp", "");
             
        }

        private string CropusArchwingTileNameFix()
        {
            return "CorpusArchwing" + rawTileNameFromMetaData.Replace("TR", "")
                                        .Replace("CorpusArchwing", "");
             
        }

        private string GasCityTileNameFix()
        {
            return "CorpusGas" + rawTileNameFromMetaData.Replace("CorpusGasCity", "")
                                        .Replace("CrpGasCity", "")
                                        .Replace("GasCity", "")
                                        .Replace("CorpusGas", "")
                                        .Replace("City", "")
                                        .Replace("Gas", "");
           
        }
    }
}
