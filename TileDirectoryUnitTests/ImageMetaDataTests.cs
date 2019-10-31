using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarframeTileDirectory;

namespace TileDirectoryUnitTests
{
    [TestClass]
    public class ImageMetaDataExtractorTests
    {
        string metaData = "/Lotus/Levels/Proc/Corpus/CorpusShipExterminate/EKEdCEI+QQEQ6hUlxfQ9M5zMyAgYRFgAQA0ACAEA.lp  Zone: /Lotus/Levels/Guild/CargoElevator2  P: -56, 5.0, -61  H:148  Log: 2975.081";
        /* The Metadata Comment in Warframe Screenshot JPGs looks like this:
         * 
         * /Lotus/Levels/Proc/Corpus/CorpusShipExterminate/EKEdCEI+QQEQ6hUlxfQ9M5zMyAgYRFgAQA0ACAEA.lp  
         * Zone: /Lotus/Levels/Guild/CargoElevator2  P: -56, 5.0, -61  H:148  Log: 2975.081
         * 
         */
        [TestMethod]
        public void TheClassCanReturnAnObjectContainingAllNeededInformationForAGivenTile()
        {
            ImageData imgData = new ImageData(metaData);

            string missionExpected = "Exterminate";
            string tilesetExpected = "CorpusShip";
            string tileExpected = "CargoElevator2";
            string missionStringExpected = "EKEdCEI+QQEQ6hUlxfQ9M5zMyAgYRFgAQA0ACAEA";
            string logExpected = "2975.081";
            string coordinatesExpected = "-56, 5.0, -61 | 148";

            Assert.AreEqual(missionExpected, imgData.Mission);
            Assert.AreEqual(tilesetExpected, imgData.Tileset);
            Assert.AreEqual(tileExpected, imgData.TileName);
            Assert.AreEqual(missionStringExpected, imgData.MissionString);
            Assert.AreEqual(logExpected, imgData.Log);
            Assert.AreEqual(coordinatesExpected, imgData.Coordinates);


        }
    }
}
