using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RLJones.FraudInspectionDriver.Classes;

namespace FraudTrackerUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetFraudId_Positive_Test()
        {
            // Arrange
            string serilaNumber = "036996202057";
            string orderNumber = "BEP060659";
            int expected_fraudId = 1717;
            FraudInspectionDb db = new FraudInspectionDb();
            db.Open();
            // ACT 
            int fraudid = db.GetFraudId(serilaNumber, orderNumber);
            // ASSERT
            Assert.AreEqual(expected_fraudId, fraudid);
        }
        [TestMethod]
        public void GetFraudId_Negative_Test()
        {
            // Arrange
            FraudInspectionDb db = new FraudInspectionDb();
            db.Open();

            string serilaNumber = "036996202057";
            string orderNumber = "BEP060659";
            int expected_fraudId = 1718;
            // ACT 
            int fraudid = db.GetFraudId(serilaNumber, orderNumber);
            // ASSERT
            Assert.AreNotEqual(expected_fraudId, fraudid);
        }
        [TestMethod]
        public void GetFraudTracker_012450291757()
        {
            // Arrange
            FraudInspectionDb db = new FraudInspectionDb();
            db.Open();
            string serilaNumber = "012450291757";
            string orderNumber = "1527451107";
            int expected_fraudid = 1684;

            // ACT 
            FraudTracker tracker = db.GetFraudTracker(serilaNumber, orderNumber);

            // ASSERT
            Assert.AreEqual(expected_fraudid, tracker.FraudId);
        }
        [TestMethod]
        public void Check_if_fraudLoop_AFCTest_IsNull()
        {
            //Arrange
            FraudInspectionDb db = new FraudInspectionDb();
            db.Open();
            string serilaNumber = "012450291757";
            string orderNumber = "1527451107";

            // ACT 
            FraudTracker tracker = db.GetFraudTracker(serilaNumber, orderNumber);

            // ASSERT
            Assert.AreEqual(null, tracker.AFCTest);
        }
        [TestMethod]
        public void Check_if_fraudLoop_PSUTest_IsNull()
        {
            //Arrange
            FraudInspectionDb db = new FraudInspectionDb();
            db.Open();
            string serilaNumber = "012450291757";
            string orderNumber = "1527451107";

            // ACT 
            FraudTracker tracker = db.GetFraudTracker(serilaNumber, orderNumber);

            // ASSERT
            Assert.AreEqual(null, tracker.PSUTest);
        }

        [TestMethod]
        public void Capture_012450291757_1527451107()
        {
            //Arrange
            FraudInspectionDb db = new FraudInspectionDb();
            FraudTracker tracker;
            db.Open();
            string serilaNumber = "015382201353";
            string orderNumber = "1526216387";
            int expectedFraudId = 1201;

            // ACT 
            // Getting the fraudloop to update
            tracker = db.GetFraudTracker(serilaNumber, orderNumber);
            // Checking the fraudid is what expected
            Assert.AreEqual(expectedFraudId, tracker.FraudId);
            // Updating AFCTest value
            db.SetTestResult(tracker.FraudId, "AFCTest", "PASS");
            db.SetCaptureDate(tracker.FraudId,DateTime.Now);
            
            // Updating fraud object data
            tracker = db.GetFraudTracker(serilaNumber, orderNumber);
            
            // ASSERT
            Assert.AreEqual("PASS", tracker.AFCTest);

            // AFTER Testing
            db.resetTests(tracker.FraudId);
        }
        [TestMethod]
        public void Check_PSUTestUpdtae()
        {
            //Arrange
            FraudInspectionDb db = new FraudInspectionDb();
            FraudTracker tracker;
            db.Open();
            string serilaNumber = "015382201353";
            string orderNumber = "1526216387";
            int expectedFraudId = 1201;

            // ACT 
            // Getting the fraudloop to update
            tracker = db.GetFraudTracker(serilaNumber, orderNumber);
            // Checking the fraudid is what expected
            Assert.AreEqual(expectedFraudId, tracker.FraudId);
            // Updating AFCTest value
            db.SetCaptureDate(tracker.FraudId, DateTime.Now);
            db.SetTestResult(tracker.FraudId, "PSUTest", "FAIL");

            // Updating fraud object data
            tracker = db.GetFraudTracker(tracker.FraudId);

            // ASSERT
            Assert.AreEqual("FAIL", tracker.PSUTest);

            // AFTER Testing
            db.resetTests(tracker.FraudId);
        }
        [TestMethod]
        public void Check_MangetTestUpdate()
        {
            //Arrange
            FraudInspectionDb db = new FraudInspectionDb();
            FraudTracker tracker;
            db.Open();
            string serilaNumber = "015382201353";
            string orderNumber = "1526216387";
            int expectedFraudId = 1201;

            // ACT 
            // Getting the fraudloop to update
            tracker = db.GetFraudTracker(serilaNumber, orderNumber);
            // Checking the fraudid is what expected
            Assert.AreEqual(expectedFraudId, tracker.FraudId);
            // Updating AFCTest value
            db.SetCaptureDate(tracker.FraudId, DateTime.Now);
            db.SetTestResult(tracker.FraudId, "MagnetTest", "PASS");

            // Updating fraud object data
            tracker = db.GetFraudTracker(tracker.FraudId);

            // ASSERT
            Assert.AreEqual("PASS", tracker.MagnetTest);

            // AFTER Testing
            db.resetTests(tracker.FraudId);
        }
    }
}
