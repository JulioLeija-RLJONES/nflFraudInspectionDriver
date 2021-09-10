using OpenQA.Selenium;
using RLJones.FraudInspectionDriver.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FraudInspectionDriver.Classes;
using System.Deployment.Application;

namespace RLJones.FraudInspectionDriver.Forms
{
    public partial class FrmMain : Form
    {   
        private FraudInspectionDb Db = new FraudInspectionDb();
        
        private bool SnValidationScreenIsActive = false;
        private bool SnValidated = false;
        private bool FraudInspectionDone = false;

        private string SerialNumber = string.Empty;
        private IWebElement SerialNumberField = null;

        private string PartNumber = string.Empty;
        private string OrderNumber = string.Empty;

        private IWebElement PartNumberField = null;
        private IWebElement OrderNumberField = null;

        public FrmMain()
        {
            InitializeComponent();
        }
        private void initForm()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                labelVersion.Text = string.Format("v{0}",
                    ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
            }
            labelVersion.Location = new Point(LblBlinker.Location.X-labelVersion.Width-labelVersion.Text.Length, LblBlinker.Location.Y / 2);
        }

        #region Controls
        private void FrmMain_Load(object sender, EventArgs e)
        {
            Db.Open();

            Rectangle workingArea = Screen.GetWorkingArea(this);

            Location = new Point(workingArea.Right - Size.Width,
                                workingArea.Top + Size.Height);

            initForm();
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Db.Dispose();
            Application.Exit();
        }
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            HandleBlinker();
            VerifyFlexLinkIsAlive();

            var body = Tools.FlexLinkChrome.WaitForElementByTagName("body", 1);

            if (body != null)
            {
                SerialNumberField =
                    Tools.FlexLinkChrome
                         .FindElementByXPath(body, "//*[@id='SerialNumber']");

                PartNumberField =
                    Tools.FlexLinkChrome
                         .FindElementByXPath(body, "//*[@id='PartNumber']");

                OrderNumberField =
                    Tools.FlexLinkChrome
                         .FindElementByXPath(body, "//*[@id='DONumber']");

                SnValidationScreenIsActive = SerialNumberField != null && PartNumberField == null;
                SnValidated = SerialNumberField != null && PartNumberField != null;

                if (SnValidationScreenIsActive)
                {
                    MainTimer.Enabled = false;
                    FraudInspectionDone = false;
                    LblStatus.Text = "Fraud inspection driver is ready...";
                    SerialNumber = SerialNumberField.GetAttribute("value").Trim();

                    MainTimer.Enabled = true;
                }
                else if (SnValidated && !FraudInspectionDone)
                {
                    DoFraudInspection();
                }
                else if (!FraudInspectionDone)
                    LblStatus.Text = "Go to 'Order Management > Screening && disposition' page...";
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.ShowDialog();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resp =
                MessageBox
                .Show("FlexLink web browser will also be closed, are you sure to exit?",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
                Close();
        }
        private void openCrossdockInspectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmCrossdockInspectionCheckpoint crossdock = new FrmCrossdockInspectionCheckpoint();
            //crossdock.Show();
            DoFraudInspection();
        }
        private void buttonTest_Click(object sender, EventArgs e)
        {
            MsgTypes.printme("Testing getting fraud loop", this);
            int fraudid = Db.GetFraudId("036996202057", "BEP060659");
            MsgTypes.printme("sn 036996202057  fraud loop = " + fraudid, this);
        }
        #endregion

        #region Logic
        /// <summary>
        /// Genreates the blink effect of the green square loaded in the User Interface.
        /// </summary>
        private void HandleBlinker()
        {
            if (LblBlinker.BackColor == Color.Lime)
                LblBlinker.BackColor = Color.Black;
            else
                LblBlinker.BackColor = Color.Lime;
        }
        /// <summary>
        /// Checks if the browser launched with webdriver is still running.
        /// </summary>
        private void VerifyFlexLinkIsAlive()
        {
            if (!Tools.FlexLinkChrome.IsAlive())
            {
                MainTimer.Enabled = false;
                MessageBox.Show("Web browser was closed", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
        /// <summary>
        /// Prompts the AFCTest result registration form and logs the result to tbl_MSFTFraud_Tracker 
        /// when result is confirmed by the operator.
        /// </summary>
        private void DoFraudInspection()
        {
            MainTimer.Enabled = false;

            PartNumber = PartNumberField.GetAttribute("value").Trim();
            OrderNumber = OrderNumberField.GetAttribute("value").Trim();
            MsgTypes.printme("captured order number: " + OrderNumber, this);

            // check if this SN was already inspected by searching a tracker entry in db...
            FraudTracker fraudTracker = Db.GetFraudTracker(SerialNumber, OrderNumber);

            // if tracker is null, SN is not inspected so we'll inspect it now:
            if (fraudTracker == null)
            {
                // check if part number is a target for fraud inspection
                var target = Db.GetInspectionTarget(PartNumber);

                if (target != null)
                {
                    // it is a target, do fraud inspection now!
                    Tools.FlexLinkChrome.Minimize();
                    LblStatus.Text = "Performing fraud inspection, SN='" + SerialNumber + "'";
                    FrmFraudInspection fraudInspection = new FrmFraudInspection(SerialNumber);
                    fraudInspection.ShowDialog();
                    fraudTracker = new FraudTracker
                    {
                        CaptureDate = DateTime.Now,
                        DeviceType = target.Class,
                        SerialNumber = SerialNumber,
                        OrderNumber = OrderNumber,
                        PartNumber = PartNumber,
                        AFCTest = fraudInspection.GetResultText()
                    };
                    MsgTypes.printme("fraud item: " + fraudTracker.ToString(), this);
                    Db.InsertFraudTracker(fraudTracker);
                    LblStatus.Text = "Fraud inspection done, SN='" + SerialNumber + "'";
                    Tools.FlexLinkChrome.Maximize();
                }
                else
                {
                    // it is not a target, show message and do nothing...
                    string msg = string.Format(
                        "SN {0} is not a target for fraud inspection.",
                        SerialNumber
                        );

                    LblStatus.Text = msg;
                }
                FraudInspectionDone = true;
            }
            else // SN is already inspected, show message and do nothing
            {

                MsgTypes.printme("Unit is already in Fraud Testing Loop", this);
                MsgTypes.printme(fraudTracker.ToString(), this);
                var selection = MessageBox.Show("Unit is already in Fraud Testing Loop. Do Retest?" + System.Environment.NewLine +
                                fraudTracker.ToString(), "Unit Under Test", MessageBoxButtons.YesNo);

                if (selection == DialogResult.Yes)
                {
                    var target = Db.GetInspectionTarget(PartNumber);
                    // Dooing Retest on demand.
                    if (target != null)
                    {
                        // it is a target, do fraud inspection now!
                        Tools.FlexLinkChrome.Minimize();
                        LblStatus.Text = "Performing fraud inspection, SN='" + SerialNumber + "'";
                        FrmFraudInspection fraudInspection = new FrmFraudInspection(SerialNumber);
                        fraudInspection.ShowDialog();
                        MsgTypes.printme("fraud item: " + fraudTracker.ToString(), this);
                        Db.SetTestResult(fraudTracker.FraudId, "AFCTest", fraudInspection.GetResultText());
                        LblStatus.Text = "Fraud inspection done, SN='" + SerialNumber + "'";
                        Tools.FlexLinkChrome.Maximize();
                    }
                    else
                    {
                        // it is not a target, show message and do nothing...
                        string msg = string.Format(
                            "SN {0} is not a target for fraud inspection.",
                            SerialNumber
                            );

                        LblStatus.Text = msg;
                    }
                }

                FraudInspectionDone = true;
            }
            MainTimer.Enabled = true;
        }
        #endregion



    }
}
