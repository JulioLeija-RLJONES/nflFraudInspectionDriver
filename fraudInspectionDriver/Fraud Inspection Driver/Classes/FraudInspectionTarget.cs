namespace RLJones.FraudInspectionDriver.Classes
{
    public class FraudInspectionTarget
    {
        public int Id { get; set; }
        /// <summary>
        /// Part number set to be identified as Fraud Inspection Target. The list of part numbers is updated by MSFT Team (Elmer Nevarez)
        /// </summary>
        public string PartNumber { get; set; }
        /// <summary>
        /// Description of the device according to Elmer Nevarez requirements.
        /// </summary>
        public string Class { get; set; }
        public string Message { get; set; }
    }
}