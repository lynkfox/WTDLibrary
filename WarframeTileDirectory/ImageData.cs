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


        private string metaData;
        private string tilesetPlusMission;

        public ImageData(string metaData)
        {
            this.metaData = metaData;
            SetMetaData();
            this.Mission = GetMission();
            this.Tileset = GetTileset();
        }

        private void SetMetaData()
        {
            string[] metaDataSplit = metaData.Split(' ');
            string[] firstSectionOfMetaData = metaDataSplit[0].Split('/');
            this.tilesetPlusMission = firstSectionOfMetaData[5];
        }

        private string GetTileset()
        {
            
            return TilesetFromMission(tilesetPlusMission);
        }

        private string TilesetFromMission(string value)
        {
            string[] tilesetArray = TileLogic.GenerateTilesetList();

            string name = tilesetArray.Where(x => value.Contains(x)).First().ToString();
            
            if(string.IsNullOrEmpty(name))
            {
                return value + "??? - Check Me: ";
            } else
            {
                return name;
            }
        }

        private string GetMission()
        {

            return GetMissionType(tilesetPlusMission);

        }

        public string GetMissionType(string value)
        {

            var types = TileLogic.GenerateMissionList();

            string name = types.Where(x => value.Contains(x.InGameName)).First().CommonName;

            //Special case consideration
            if (value == "CorpusShip")
            {
                return "Assassination (The Sergeant)";

            }else if (value == "GrineerAsteroid")
            {
                return "Sabotage (Drill)";

            }else if (string.IsNullOrEmpty(name))
            {
                return value + "??? - Check Me: ";

            } else
            {
                return name;
            }

        }

    }
}
