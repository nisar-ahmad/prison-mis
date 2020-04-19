using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DPUruNet;
using System.IO;

namespace PRSFingerIdentify
{
    public partial class IdentifyDlg : Form
    {
        //private IdentificationWrapper _idWrapper = null;
        private Reader reader;
        private Fmd fmd = null;
        private delegate void Function();  //Used to declare anyonymous methods for updating GUI from biometric event thread
        
        private string _serverIP;
        ///WebBrowser browser;

        public IdentifyDlg()
        {
            InitializeComponent();

            _serverIP = File.ReadAllText("ServerIP.txt");
            HelperFunctions.connectionString = string.Format(@"Data Source={0}; Initial Catalog=UNODCPRS; User ID=sa; Password=sa123;", _serverIP);
        }

        void reader_On_Captured(CaptureResult result)
        {
            if (result.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Capture failed.");
                return;
            }

            Fid fid = result.Data;

            //Display captured image
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Function(delegate
                {
                    pbImage.Image = new Bitmap(Fid2Bitmap.CreateBitmap(result.Data.Views[0].Bytes, fid.Views[0].Width, fid.Views[0].Height), pbImage.Size);
                }));
            }

            //Extract pre-registration features
            DataResult<Fmd> fmdResult = FeatureExtraction.CreateFmdFromFid(result.Data, Constants.Formats.Fmd.DP_VERIFICATION);

            if (fmdResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Error extracting features from image.  Please try again");
                return;
            }
            else
                fmd = fmdResult.Data;

            // Lets identify the print

            // Perform indentification of fmd of captured sample against enrolledFmds for userid 
            IdentifyResult iResult1 = Comparison.Identify(fmd, 0, HelperFunctions.GetAllFmd1s, 21474, 1);

            // If Identify was successful
            if (iResult1.ResultCode == Constants.ResultCode.DP_SUCCESS)
            {
                //If number of matches were greater than 0
                if (iResult1.Indexes.Length == 1)
                {
                    string[] usernames = HelperFunctions.GetAllPrisonerIds;
                    string prisonerId = usernames[iResult1.Indexes[0][0]];

                    string ip = (_serverIP == "(local)" ? "localhost" : _serverIP);

                    string url = string.Format("http://{0}/PMIS/Prisoner/Details?PrisonerId={1}", ip, prisonerId);

                    //if(browser == null)
                    //    browser = new WebBrowser();

                    //browser.Navigate(url, "_prs");
                    //browser.Document.Window.Open(url, "_prs", null, true);

                    //window.Open(url, "_prs", null, true);
                    System.Diagnostics.Process.Start(url);
                }
                else if (iResult1.Indexes.Length > 1)
                {
                    string ids = "";

                    string[] usernames = HelperFunctions.GetAllPrisonerIds;

                    for (int i = 0; i < iResult1.Indexes.Length; i++)
                    {
                        ids += usernames[iResult1.Indexes[i][0]] + ", ";
                    }

                    MessageBox.Show("Error: Multiple matches found. Prisoner IDs : {0}", ids);
                }
                else
                    MessageBox.Show("Prisoner NOT found! Try again or enroll prisoner!");
            }
            else
                MessageBox.Show("Error : " + iResult1.ResultCode.ToString());

            pbImage.Image = null;
        }

        //In Load lets enumerate and open fingerprint reader
        private void IdentifyDlg_Load(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            ReaderCollection readers = ReaderCollection.GetReaders();
            if (readers.Count == 0)
            {
                MessageBox.Show("Registration requires a plugged in fingerprint reader.");
                readers.Dispose();
                readers = null;
                this.Close();
                return;
            }

            if (readers.Count > 1)
            {
                MessageBox.Show("This sample is designed for a single connected reader.  Please connect only 1 device.");
                readers.Dispose();
                readers = null;
                this.Close();
                return;
            }

            reader = readers[0];

            if (reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE) != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Could not open device.");
                readers.Dispose();
                readers = null;
                this.Close();
                return;
            }

            if (reader.GetStatus() != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Error getting device status.");
                readers.Dispose();
                readers = null;
                this.Close();
                return;
            }

            if (reader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY)
            {
                MessageBox.Show("Device not ready.  Try again in a few seconds.");
                readers.Dispose();
                readers = null;
                this.Close();
                return;
            }

            HelperFunctions.LoadAllUsers();

            reader.On_Captured += new Reader.CaptureCallback(reader_On_Captured);

            Constants.ResultCode captureResult = reader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, reader.Capabilities.Resolutions[0]);

            if (captureResult != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Error CaptureResult: " + captureResult.ToString());
            }
        }

        private void IdentifyDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Release fingerprint reader so it can be used by another form.
            reader.Dispose();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            pbImage.Image = null;
            HelperFunctions.LoadAllUsers();
        }
    }
}
