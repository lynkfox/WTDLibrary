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

        public ImageData(string metaData)
        {
            this.metaData = metaData;
            this.Mission = GetMission();
        }

        private string GetMission()
        {
            string[] metaDataSplit = metaData.Split(' ');
            string[] firstSectionOfMetaData = metaDataSplit[0].Split('/');
            string tilesetPlusMission = firstSectionOfMetaData[5];

            return GetMissionType(tilesetPlusMission);

        }

        public string GetMissionType(string value)
        {

            var types = TileLogic.GenMissionList();

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
                return value + "??? - Check Me: " + value;
            } else
            {
                return name;
            }

        }

    }
}
