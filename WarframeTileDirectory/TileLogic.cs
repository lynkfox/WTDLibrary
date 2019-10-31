using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WarframeTileDirectory
{
    public static class TileLogic
    {
        public static List<MissionType> GenMissionList()
        {
            List<MissionType> list = new List<MissionType>();

            var namePairs = Properties.Resources.MissionNames.Split('\n');

            

            foreach (var line in namePairs)
            {
                if(!string.IsNullOrWhiteSpace(line))
                {
                    var mission = new MissionType();
                    string[] splitPairs = line.Split(',');
                    mission.CommonName = splitPairs[1].Remove(splitPairs[1].Length - 1);
                    mission.InGameName = splitPairs[0];
                    list.Add(mission);
                }
                
            }

            return list;
        }
    }
}
