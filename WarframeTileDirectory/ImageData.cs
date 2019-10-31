using System;

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
            return "Exterminate";
        }

        
    }
}
