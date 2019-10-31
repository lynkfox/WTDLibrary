using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarframeTileDirectory;

namespace TileDirectoryUnitTests
{
    [TestClass]
    public class ImageMetaDataExtractorTests
    {
        string missionExpected = "Exterminate";
        string tilesetExpected = "CorpusShip";
        string tileExpected = "CargoElevator2";
        string missionStringExpected = "EKEdCEI+QQEQ6hUlxfQ9M5zMyAgYRFgAQA0ACAEA";
        string logExpected = "2975.081";
        string coordinatesExpected = "-56, 5.0, -61 | 148";

        string metaData = "/Lotus/Levels/Proc/Corpus/CorpusShipExterminate/EKEdCEI+QQEQ6hUlxfQ9M5zMyAgYRFgAQA0ACAEA.lp  Zone: /Lotus/Levels/Guild/CargoElevator2  P: -56, 5.0, -61  H:148  Log: 2975.081";
        /* The Metadata Comment in Warframe Screenshot JPGs looks like this:
         * 
         * /Lotus/Levels/Proc/Corpus/CorpusShipExterminate/EKEdCEI+QQEQ6hUlxfQ9M5zMyAgYRFgAQA0ACAEA.lp  
         * Zone: /Lotus/Levels/Guild/CargoElevator2  P: -56, 5.0, -61  H:148  Log: 2975.081
         * 
         */


        string secondMetaData = "/Lotus/Levels/Proc/Orokin/OrokinMoonSurvival/JKhLQlALNQMw2TwsGnoIaejD2QiOABTAAAQBAEAAgAA.lp  Zone: /Lotus/Levels/OrokinMoon/MoonSpawn03  P: -166, -13, -158  H:15  Log: 17218.486";
        string missionExpectedOther = "Surviva";
        string tilesetExpectedOther = "OrokinMoon";
        string tileExpectedOther = "MoonSpawn03";
        string missionStringExpectedOther = "JKhLQlALNQMw2TwsGnoIaejD2QiOABTAAAQBAEAAgAA";
        string logExpectedOther = "17218.486";
        string coordinatesExpectedOther = "-166, -13, -158 | 15";

        [TestMethod]
        public void TheClassCanReturnAnObjectContainingAllNeededInformationForAGivenTile()
        {
            ImageData imgData = new ImageData(metaData);

            Assert.AreEqual(missionExpected, imgData.Mission);
            Assert.AreEqual(tilesetExpected, imgData.Tileset);
            Assert.AreEqual(tileExpected, imgData.TileName);
            Assert.AreEqual(missionStringExpected, imgData.MissionString);
            Assert.AreEqual(logExpected, imgData.Log);
            Assert.AreEqual(coordinatesExpected, imgData.Coordinates);

        }

        [TestMethod]
        public void ExtractionCanBePulledFromMetaDataAsMission()
        {
            ImageData imgData = new ImageData(metaData);

            Assert.AreEqual(missionExpected, imgData.Mission);
        }
    }
}
