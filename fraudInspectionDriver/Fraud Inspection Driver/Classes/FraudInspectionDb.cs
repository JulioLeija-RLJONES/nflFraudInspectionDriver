using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLJones.FraudInspectionDriver.Classes
{
    public class FraudInspectionDb : SqlHelper
    {
        /// <summary>
        /// Class carries out all SQL transactions between the user interface and the SQL database in Azure
        /// </summary>
        public FraudInspectionDb()
            : base(ApplicationDeployment.IsNetworkDeployed ? "ProductionDatabase" : "DebugDatabase")
        {

        }
        /// <summary>
        /// Queries table tbl_MSFTFraud_Tracker in elpuatsqlserver.database.windows.net db_elptest and pulls rows matching 
        /// SerialNumber and OrderNumber fields
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <param name="OrderNumber"></param>
        /// <returns>The unique identifier of the first result found in the result table.</returns>
        public int GetFraudId(string SerialNumber,string OrderNumber)
        {
            FraudTracker fraudloop = null;
            string sql = "SELECT * FROM dbo.tbl_MSFTFraud_Tracker " +
                "WHERE " +
                "SerialNumber = @SerialNumber " +
                "AND OrderNumber = @OrderNumber";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SerialNumber",SerialNumber),
                new SqlParameter("@OrderNumber",OrderNumber)
            };
            var data = ExecuteReader(sql, parameters);
           if(data.Count>0)
            {
                var row = data[0];
                fraudloop = new FraudTracker()
                {
                    FraudId = Convert.ToInt32(row.FieldValues[0])
                };
            }
            return fraudloop.FraudId;
        }
        /// <summary>
        /// Queries table tbl_MSFTFraud_Tracker in elpuatsqlserver.database.windows.net db_elptest and pulls rows matching 
        /// SerialNumber and OrderNumber fields.
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <param name="OrderNumber"></param>
        /// <returns>The FraidTracker object containing all fields of Tracker database</returns>
        public FraudTracker GetFraudTracker(string SerialNumber, string OrderNumber)
        {
            FraudTracker tracker = null;
            string sql = "SELECT * FROM dbo.tbl_MSFTFraud_Tracker " +
                          "WHERE " +
                          "SerialNumber = @SerialNumber " +
                          "AND OrderNumber = @OrderNumber";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SerialNumber",SerialNumber),
                new SqlParameter("@OrderNumber",OrderNumber)
            };
            var data = ExecuteReader(sql, parameters);

            if (data.Count > 0)
            {
                var row = data[0];
                tracker = new FraudTracker()
                {
                    /*
                     * Commented properties will be updated by azure cloud job
                     */
                    FraudId = Convert.ToInt32(row.FieldValues[0]),
                    //UnitId = Convert.ToInt32(row.FieldValues[1]),
                    PartNumber = row.FieldValues[2].ToString(),
                    //Family = row.FieldValues[3].ToString(),
                    //IsFraudTarget = Convert.ToInt32(row.FieldValues[4]),
                    //ReceivedDate = Convert.ToDateTime(row.FieldValues[5]),
                    //Channel = row.FieldValues[6].ToString(),
                    FraudLoop = Convert.ToInt32(row.FieldValues[9]),
                    DeviceType = row.FieldValues[2].ToString(),
                    BlueScreenInspection = row.FieldValues[14].ToString(),
                    SerialNumber = row.FieldValues[3].ToString(),
                    IsCaptured =  Convert.ToInt32(row.FieldValues[7])
                };

                if (row.FieldValues[8] != System.DBNull.Value)
                {
                    tracker.CaptureDate = Convert.ToDateTime(row.FieldValues[8]);
                    tracker.IsFraudLoopComplete =  Convert.ToInt32(row.FieldValues[15]);
                    tracker.AFCTest = row.FieldValues[11].ToString();
                    tracker.PSUTest = row.FieldValues[12].ToString();
                    tracker.MagnetTest = row.FieldValues[13].ToString();
                }
                


            }
            return tracker;
        }
        /// <summary>
        /// Queries table tbl_MSFTFraud_Tracker in elpuatsqlserver.database.windows.net db_elptest and pulls rows matching 
        /// the unique identified provided.
        /// </summary>
        /// <param name="fraudid"></param>
        /// <returns>The FraidTracker object containing all fields of Tracker database</returns>
        public FraudTracker GetFraudTracker(int fraudid)
        {
            FraudTracker tracker = null;
            string sql = "SELECT * FROM dbo.tbl_MSFTFraud_Tracker " +
                          "WHERE " +
                          "FraudId = @FraudId";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FraudId",fraudid)
            };
            var data = ExecuteReader(sql, parameters);

            if (data.Count > 0)
            {
                var row = data[0];
                tracker = new FraudTracker()
                {
                    FraudId = Convert.ToInt32(row.FieldValues[0]),
                    UnitId = Convert.ToInt32(row.FieldValues[1]),
                    PartNumber = row.FieldValues[2].ToString(),
                    Family = row.FieldValues[3].ToString(),
                    IsFraudTarget = Convert.ToInt32(row.FieldValues[4]),
                    ReceivedDate = Convert.ToDateTime(row.FieldValues[5]),
                    Channel = row.FieldValues[6].ToString(),
                    FraudLoop = Convert.ToInt32(row.FieldValues[9]),
                    DeviceType = row.FieldValues[2].ToString(),
                    BlueScreenInspection = row.FieldValues[14].ToString(),
                    SerialNumber = row.FieldValues[3].ToString(),
                    IsCaptured = Convert.ToInt32(row.FieldValues[7])
                };

                if (row.FieldValues[8] != System.DBNull.Value)
                {
                    tracker.CaptureDate = Convert.ToDateTime(row.FieldValues[8]);
                    tracker.IsFraudLoopComplete = Convert.ToInt32(row.FieldValues[15]);
                    tracker.AFCTest = row.FieldValues[11].ToString();
                    tracker.PSUTest = row.FieldValues[12].ToString();
                    tracker.MagnetTest = row.FieldValues[13].ToString();
                }



            }
            return tracker;
        }
        /// <summary>
        /// Inserts a new row in tbl_MSFTFraud_Tracker using the fields of a FraudTracker object.
        /// </summary>
        /// <param name="tracker"></param>
        public void InsertFraudTracker(FraudTracker tracker)
        {
            string sql = "INSERT INTO dbo.tbl_MSFTFraud_Tracker";
            sql += "(CapturedDate, DeviceType, SerialNumber, OrderNumber,PartNumber, AFCTest,IsCaptured,FraudLoop,IsFraudLoopComplete)";
            sql += "VALUES(@CapturedDate, @DeviceType, @SerialNumber,@OrderNumber,@PartNumber, @AFCTest,@IsCaptured,@FraudLoop,@IsFraudLoopComplete)";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@IsCaptured", 1),
                new SqlParameter("@CapturedDate", DateTime.Now),
                new SqlParameter("@DeviceType", tracker.DeviceType),
                new SqlParameter("@SerialNumber", tracker.SerialNumber),
                new SqlParameter("@OrderNumber", tracker.OrderNumber),
                new SqlParameter("@PartNumber", tracker.PartNumber),
                new SqlParameter("@AFCTest", tracker.AFCTest),
                new SqlParameter("@FraudLoop", 1),
                new SqlParameter("@IsFraudLoopComplete", 0)
            };

            ExecuteReader(sql, parameters);
        }
        /// <summary>
        /// Updates the result of the test defined by testName in tbl_MSFTFraud_Tracker in the row matching
        /// the unique identifier provided.
        /// </summary>
        /// <param name="fraudId"></param>
        /// <param name="testName"></param>
        /// <param name="result"></param>
        public void SetTestResult(int fraudId,string testName,string result)
        {
            string sql = GetTestSQL(testName);
            Console.WriteLine(sql);
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Result",result),
                new SqlParameter("@FraudId",fraudId)
            };
         
            ExecuteReader(sql, parameters);
        }
        /// <summary>
        /// Selects the UPDATE statement to use given the testname
        /// </summary>
        /// <param name="testname"></param>
        /// <returns>Predefined UPDATE SQL statement</returns>
        public string GetTestSQL(string testname)
        {
            string sql = String.Empty;
            switch (testname)
            {
                case "AFCTest":
                    sql =  "UPDATE dbo.tbl_MSFTFraud_Tracker " +
                         "SET AFCTest = @Result " +
                         "WHERE FraudId = @FraudId";
                    break;
                case "PSUTest":
                    sql = "UPDATE dbo.tbl_MSFTFraud_Tracker " +
                        "SET PSUTest = @Result " +
                        "WHERE FraudId = @FraudId";
                    break;
                case "MagnetTest":
                    sql = "UPDATE dbo.tbl_MSFTFraud_Tracker " +
                        "SET MagnetTest = @Result " +
                        "WHERE FraudId = @FraudId";
                    break;
              case "BlueScreenInspection":
                    sql = "UPDATE dbo.tbl_MSFTFraud_Tracker " +
                        "SET BlueScreenInspection = @Result " +
                        "WHERE FraudId = @FraudId";
                    break;
            }
            return sql;
        }
        /// <summary>
        /// Updates the CaptureDAte field of the row matching the unique identifier fraudid in tbl_MSFTFraud_Tracker
        /// </summary>
        /// <param name="fraudid"></param>
        /// <param name="captureDate"></param>
        public void SetCaptureDate(int fraudid, DateTime captureDate)
        {
            string sql = "UPDATE dbo.tbl_MSFTFraud_Tracker " +
                         "SET CapturedDate = @CaptureDate " +
                         "WHERE FraudId = @FraudId";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CaptureDate",captureDate),
                new SqlParameter("@FraudId",fraudid)
            };
            ExecuteReader(sql, parameters);
        }
        /// <summary>
        /// Retrieves the reuslto of AFCTest of the row matching the given unique identifier fraudid.
        /// </summary>
        /// <param name="fraudid"></param>
        /// <returns>AFTest result.</returns>
        public string GetAFCTestResult(int fraudid)
        {
            FraudTracker tracker = GetFraudTracker(fraudid);
            return tracker.AFCTest;
        }
        /// <summary>
        /// Updates AFCTest,PSUTest, MagnetTest, 
        /// </summary>
        /// <param name="fraudid"></param>
        public void resetTests(int fraudid)
        {
            string sql = "UPDATE dbo.tbl_MSFTFraud_Tracker " +
                         "SET IsCaptured = 0, CapturedDate = NULL, " +
                         "AFCTest = NULL, PSUTest = NULL, " +
                         "MagnetTest = NULL, BlueScreenInspection = NULL, " +
                         "IsFraudLoopComplete = 0 " +
                         "WHERE FraudId = @FraudId";
            
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FraudId",fraudid)
            };
            ExecuteReader(sql, parameters);
        }



        #region Legacy Code
        /*
         * Legacy Code
         * Functions and queries of the previous version of the Fraud Tracker database
         */
        /// <summary>
        /// Queries FraudInspectionTargets table in db_elptest to extract rows matching the partNumber provided.
        /// </summary>
        /// <param name="partNumber"></param>
        /// <returns>A FraudInspectin Target containing Id, PartNumber, Class data.</returns>
        public FraudInspectionTarget GetInspectionTarget(string partNumber)
        {
            FraudInspectionTarget target = null;
            // force to remove all unwanted characters from PartNumber string in db
            string sql = "SELECT * FROM FraudInspectionTargets WHERE ";
            sql += "LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(PartNumber, ";
            sql += "CHAR(10), CHAR(32)), CHAR(13), CHAR(32)), CHAR(160), CHAR(32)), CHAR(9), CHAR(32))))";
            sql += "='" + partNumber + "'";
            var datas = ExecuteReader(sql);
            foreach(var data in datas)
            {
                target = new FraudInspectionTarget
                {
                    Id = Convert.ToInt32(data.FieldValues[0]),
                    PartNumber = data.FieldValues[1].ToString().Replace("\n\r", "").Trim(),
                    Class = data.FieldValues[2].ToString(),
                    Message = data.FieldValues[3].ToString()
                };
            }
            return target;
        }
        /// <summary>
        /// QUeries FraudTracker table to search for the MagnetTest result of the first matched row containing given SerialNumber.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns>True if the test is PASS or False if the test is Fail.</returns>
        public bool IsMagnetPass(string serialNumber)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SerialNumber", serialNumber)
            };
            var datas =
               ExecuteReader
               ("SELECT * FROM FraudTracker WHERE SerialNumber=@SerialNumber ORDER BY FraudId DESC", parameters);


            foreach (var data in datas)
            {
                Console.WriteLine(serialNumber + " Line: " + data.FieldValues[0] + ", " + data.FieldValues[1] +
                                                        ", " + data.FieldValues[2] +
                                                        ", " + data.FieldValues[3] +
                                                        ", " + data.FieldValues[4] +
                                                        ", " + data.FieldValues[5] +
                                                        ", " + data.FieldValues[6]);
            }

            return true;
        }

        /// <summary>
        /// Queries FraudTracker in db_elptest to extract data of the first match found in the table with given SerialNumber.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns>a FraudTracker object containing data of the capture.</returns>
        //public FraudTracker GetFraudTracker(string serialNumber)
        //{
        //    FraudTracker tracker = null;

        //    var parameters = new List<SqlParameter>
        //    {
        //        new SqlParameter("@SerialNumber", serialNumber)
        //    };

        //    var datas =
        //        ExecuteReader
        //        ("SELECT * FROM FraudTracker WHERE SerialNumber=@SerialNumber", parameters);

        //    foreach (var data in datas)
        //    {
        //        tracker = new FraudTracker
        //        {
        //            FraudId = Convert.ToInt32(data.FieldValues[0]),
        //            CaptureDate = Convert.ToDateTime(data.FieldValues[1]),
        //            DeviceType = data.FieldValues[2].ToString(),
        //            SerialNumber = data.FieldValues[3].ToString(),
        //            PSUTest = data.FieldValues[4].ToString(),
        //            MagnetTest = data.FieldValues[5].ToString(),
        //            ManualCID = data.FieldValues[6].ToString(),   
        //        };
        //    }
        //    return tracker;
        //}
        /// <summary>
        /// Inserts new row in FraudTracker on db_elptest.
        /// </summary>
        /// <param name="tracker"></param>
        //public void InsertFraudTracker(FraudTracker tracker)
        //{
        //    string sql = "INSERT INTO FraudTracker";
        //    sql += "(Date, DeviceType, SerialNumber, PSUTest) ";
        //    sql += "VALUES(@Date, @DeviceType, @SerialNumber, @PSUTest)";

        //    var parameters = new List<SqlParameter>
        //    {
        //        new SqlParameter("@Date", DateTime.Now),
        //        new SqlParameter("@DeviceType", tracker.DeviceType),
        //        new SqlParameter("@SerialNumber", tracker.SerialNumber),
        //        new SqlParameter("@PSUTest", tracker.PSUTest)
        //    };

        //    ExecuteReader(sql, parameters);
        //}

        #endregion




    }
}
