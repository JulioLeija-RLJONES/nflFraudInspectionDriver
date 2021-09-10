using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using RLJones.FraudInspectionDriver.Classes;

namespace RLJones.FraudInspectionDriver.Forms
{
    public partial class FrmCrossdockInspectionCheckpoint : Form
    {
        int msg_pass = 2;
        int msg_fail = 3;
        string hostname ;
        int sessionCount = 0;

        private CrossdockInspectionDb crossdockDB = new CrossdockInspectionDb();




        public FrmCrossdockInspectionCheckpoint()
        {
            InitializeComponent();
        }
        private void FrmCrossdockInspectionCheckpoint_Load(object sender, EventArgs e)
        {
            SetHostname();
            printme(msg_pass, "hostname " + GetStation(hostname));
            lblStationName.Text = "Worstation: " + GetStation(hostname);
            initForm();
            printme(msg_pass, "app ready");
        }
        private void initForm()
        {
            textBox_orderNumber.Select();
            //this.WindowState = FormWindowState.Minimized;
        }

        #region Logic
        
        private bool isGoodOrder(string orderNumber)
        {
            Regex regex = new Regex(@"\d{10}");
            Match match = regex.Match(orderNumber);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private string GetStation(string hostname)
        {
            string stationName = ""; 

            switch (hostname)
            {
                case "RLJ-ELP-L31":
                    stationName = "Master";
                    break;

                default:
                    stationName = hostname;
                    break;
            }

            return stationName;
        }
        private void SetHostname()
        {
            hostname = Dns.GetHostName();
            printme(msg_pass, "hostname set");
        }
        private void ResetInputs()
        {
            textBox_orderNumber.Clear();
            textBox_orderNumber.Select();
        }
        private void LogInspectionAction()
        {
            if (isGoodOrder(GetOrderNumber()))
            {
                crossdockDB.InsertCrossdockInspection(GetOrderNumber(), GetStation(hostname));
                printme(msg_pass, "order " + GetOrderNumber() + " registered.");
                sessionCount += 1;
                lblsessionCount.Text = "Session count: " + sessionCount;
                ResetInputs();
            }
            else
            {
                printme(msg_fail, "order number " + GetOrderNumber() + " has invalid format. Please scan again order.");
                ResetInputs();
            }
        }

        #endregion


        #region Debug
        private void printme(int msgType ,string  msg)
        {
            switch(msgType)
            {
                case 2:
                    prompt.AppendText("success >> " + msg + System.Environment.NewLine) ;
                    prompt.ScrollToCaret();
                    break;
                case 3:
                    prompt.AppendText("failure >> " + msg + System.Environment.NewLine);
                    prompt.ScrollToCaret();
                    break;
            }
        }
        #endregion

        #region Controls
        private string GetOrderNumber()
        {
            return textBox_orderNumber.Text;
        }
        


        private void textBox_orderNumber_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LogInspectionAction();
            }
        }
        #endregion


    }
}
