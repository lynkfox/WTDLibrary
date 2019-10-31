using System;

namespace WarframeTileDirectory
{
    public class ImageData
    {
        private string metaData;

        public ImageData(string metaData)
        {
            this.metaData = metaData;
        }

        public string Mission { get; set; }
        public string Tileset { get; set; }
        public string TileName { get; set; }
        public string MissionString { get; set; }
        public string Log { get; set; }
        public string Coordinates { get; set; }
    }
}
