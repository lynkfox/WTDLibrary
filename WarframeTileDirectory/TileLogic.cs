using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WarframeTileDirectory
{
    public static class TileLogic
    {
        public static List<MissionType> GenerateMissionList()
        {
            List<MissionType> list = new List<MissionType>();

            var namePairs = Properties.Resources.MissionNames.Split('\n');

            foreach (var line in namePairs)
            {
                if(!string.IsNullOrWhiteSpace(line))
                {
                    list.Add(TranslateFileLineIntoMissionType(line));
                }
            }

            return list;
        }

        public static string[] GenerateTilesetList()
        {
            return Properties.Resources.TilesetNames.Split(',');
        }

        private static MissionType TranslateFileLineIntoMissionType(string line)
        {
            
            string[] splitPairs = line.Split(',');

            return new MissionType()
            {
                CommonName = splitPairs[1].Remove(splitPairs[1].Length - 1),
                InGameName = splitPairs[0]
            };
        }
    }
}
