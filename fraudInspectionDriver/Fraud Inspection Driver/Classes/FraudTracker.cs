using System;

namespace RLJones.FraudInspectionDriver.Classes
{
    public class FraudTracker
    {
        public int FraudId { get; set; }
        /// <summary>
        /// This value is calculated by automatic job that will be completing device data of each capture with a background job.
        /// </summary>
        public int UnitId { get; set; }
        public string PartNumber { get; set; }
        /// <summary>
        /// This value is calculated by automatic job that will be completing device data of each capture with a background job.
        /// </summary>
        public string Family { get; set; }
        /// <summary>
        /// This value is calculated by automatic job that will be completing device data of each capture with a background job.
        /// </summary>
        public int IsFraudTarget { get; set; }
        /// <summary>
        /// This value is calculated by automatic job that will be completing device data of each capture with a background job.
        /// </summary>
        public DateTime ReceivedDate { get; set; }
        /// <summary>
        /// This value is calculated by automatic job that will be completing device data of each capture with a background job.
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// Value set to 1 by the capture tool when completing the AFCTest
        /// </summary>
        public int IsCaptured { get; set; }
        /// <summary>
        /// Date when the device completed the AFCTest register.
        /// </summary>
        public DateTime CaptureDate { get; set; }
        /// <summary>
        /// Test loop of the device. 
        /// </summary>
        public int FraudLoop { get; set; }
        /// <summary>
        /// Device description as per Elmer Nevarez requirement, pulled from FraudInspectionTargets table in db_elptest.
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// Customer Induced Damage inspection result.
        /// </summary>
        public string ManualCID { get; set; }
        /// <summary>
        /// Anti Fraud Check test result.Performed at MSFT receiving station.
        /// </summary>
        public string AFCTest { get; set; }
        /// <summary>
        /// Power Supply Test Result.Performed at MSFT receiving station.
        /// </summary>
        public string PSUTest { get; set; }
        /// <summary>
        /// Magnet Test result.Performed at MSFT receiving station.
        /// </summary>
        public string MagnetTest { get; set; }
        /// <summary>
        /// Blue Screen inspection result.Performed at MSFT receiving station.
        /// </summary>
        public string BlueScreenInspection { get; set; }
        /// <summary>
        /// Blue Screen inspection result. Performed at MSFT receiving station.
        /// </summary>
        public int IsFraudLoopComplete { get; set; }
        /// <summary>
        /// Serial number read from the device at inspection time.
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// This value is calculated by automatic job that will be completing device data of each capture with a background job.
        /// </summary>
        public string ReceivingUser { get; set; }
        /// <summary>
        /// Value extracted at inspection time from the web document.
        /// </summary>
        public string OrderNumber { get; set; }


        public override string ToString()
        {
            return "sn: " + this.SerialNumber + " order: " + this.OrderNumber + " tests: [" + this.AFCTest + ", " +
                this.PSUTest + ", " + this.MagnetTest + ", " + this.BlueScreenInspection +"]" ;
        }


        #region Legacy Code
        /*
         * Legacy Code
         * Not fitting into new database schema
         */
        //public DateTime Date { get; set; }
        #endregion
    }
}