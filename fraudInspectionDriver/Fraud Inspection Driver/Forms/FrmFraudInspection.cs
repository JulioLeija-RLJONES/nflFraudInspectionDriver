using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLJones.FraudInspectionDriver
{
    public partial class FrmFraudInspection : Form
    {
        private readonly string SerialNumber;
        private InspectionResult Result = InspectionResult.NONE;

        /// <summary>
        /// List of possible results that can be inserted as result in Fraud Tests.
        /// </summary>
        public enum InspectionResult
        {
            NONE,
            FAIL,
            PASS,
            BYPASS
        }
        
        public FrmFraudInspection(string serialNumber)
        {
            InitializeComponent();
            SerialNumber = serialNumber;
            LblSerialNumber.Text = "SN: " + serialNumber;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void RadPass_CheckedChanged(object sender, EventArgs e)
        {
            Result = InspectionResult.PASS;
            BtnConfirm.Enabled = RadPass.Checked || RadFail.Checked || RadByPass.Checked;
        }

        private void RadFail_CheckedChanged(object sender, EventArgs e)
        {
            Result = InspectionResult.FAIL;
            BtnConfirm.Enabled = RadPass.Checked || RadFail.Checked || RadByPass.Checked;
        }
        private void RadByPass_CheckedChanged(object sender, EventArgs e)
        {
            Result = InspectionResult.BYPASS;
            BtnConfirm.Enabled = RadPass.Checked || RadFail.Checked || RadByPass.Checked;
        }
        /// <summary>
        /// Parses enum test value to String. 
        /// <br/><br/>
        /// Remarks:
        /// <br/>
        /// The tests result is store as String in the database.
        /// </summary>
        /// <returns></returns>
        public string GetResultText()
        {
            switch(Result)
            {
                case InspectionResult.PASS:
                    return "PASS";
                case InspectionResult.FAIL:
                    return "FAIL";
                case InspectionResult.BYPASS:
                    return "BYPASS";
                default:
                    return "NONE";
            }
        }
    }
}
