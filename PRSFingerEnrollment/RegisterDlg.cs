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
using System.Net.Http;
using System.Drawing.Imaging;
using System.Net.Http.Headers;

namespace PRSFingerIdentify
{
    public static class ImageExtensions
    {
        public static Stream ToStream(this Image image, ImageFormat format)
        {
            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
    }

    public partial class RegisterDlg : Form
    {
        public Fmd leftIndex = null;
        public Fmd fmd1 = null;
        private List<Fmd> listPreRegFMDs = new List<Fmd>();
        private int pressCount = 0;
        private Reader reader;
        private delegate void Function(); //Used to declare anyonymous methods for updating GUI from biometric event thread
        private string _serverIP;

        public RegisterDlg()
        {
            InitializeComponent();

            _serverIP = File.ReadAllText("ServerIP.txt");
            HelperFunctions.connectionString = string.Format(@"Data Source={0}; Initial Catalog=UNODCPRS; User ID=sa; Password=sa123;", _serverIP);
        }

        void reader_On_Captured(CaptureResult result)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Function(delegate
                {
                    if (result.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        MessageBox.Show("Capture failed.");
                        return;
                    }

                    Fid fid = result.Data;

                    //Extract pre-registration features
                    DataResult<Fmd> fmd = FeatureExtraction.CreateFmdFromFid(result.Data, Constants.Formats.Fmd.DP_PRE_REGISTRATION);
                    if (fmd.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        MessageBox.Show("Error extracting features from image.  Please try again");
                        return;
                    }

                    listPreRegFMDs.Add(fmd.Data);  //Add good image to list of fids 

                    //Attempt to create enrollment Fmd from list of fids
                    DataResult<Fmd> enrollmentFMD = Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.DP_REGISTRATION, listPreRegFMDs);

                    if (enrollmentFMD.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                        MessageBox.Show("Enrollment failed.  Please try again.");
                        pressCount = 0;
                        pb1.Image = null;
                        pb2.Image = null;
                        pb3.Image = null;
                        pb4.Image = null;
                        //reader.CaptureAsync(Constants.Formats.Fid.ISO, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, reader.Capabilities.Resolutions[0]);
                        return;
                    }

                    if (pressCount == 0)
                        pb1.Image = new Bitmap(Fid2Bitmap.CreateBitmap(result.Data.Views[0].Bytes, fid.Views[0].Width, fid.Views[0].Height), pb1.Size);
                    else if (pressCount == 1)
                        pb2.Image = new Bitmap(Fid2Bitmap.CreateBitmap(fid.Views[0].Bytes, fid.Views[0].Width, fid.Views[0].Height), pb2.Size);
                    else if (pressCount == 2)
                        pb3.Image = new Bitmap(Fid2Bitmap.CreateBitmap(fid.Views[0].Bytes, fid.Views[0].Width, fid.Views[0].Height), pb3.Size);
                    else if (pressCount == 3) //Dont increment.  Say on last picture box until successfull registration
                        pb4.Image = new Bitmap(Fid2Bitmap.CreateBitmap(fid.Views[0].Bytes, fid.Views[0].Width, fid.Views[0].Height), pb4.Size);

                    ++pressCount;

                    if (enrollmentFMD.ResultCode == Constants.ResultCode.DP_SUCCESS) //enrollment FMD created
                    {
                        //if (rbLeftIndex.Checked)
                        //{
                        //    leftIndex = enrollmentFMD.Data;
                        //    //rbRightIndex.Checked = true;
                        //    lblInfo.Text = "Now press your right index 4 or more times.";
                        //    pb1.Image = null;  pb2.Image = null; pb3.Image = null; pb4.Image = null;
                        //    pressCount = 0;
                        //    listPreRegFMDs.Clear();
                        //}
                        //else
                        //{
                        fmd1 = enrollmentFMD.Data;
                        btnSave.Enabled = true;
                        //reader.CancelCapture();
                        //MessageBox.Show("Fingerprint enrollment complete.  Click 'Save' to complete registration."); 
                        //}

                    }

                }));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var computerNo = tbName.Text.Trim();

            if (computerNo.Length == 0)
            {
                MessageBox.Show("Please enter Computer Number");
                return;
            }

            string xmlFMD1 = Fmd.SerializeXml(fmd1);

            string saveFmdScript = "UPDATE Prisoners SET FMD1='" + xmlFMD1 + "' WHERE PrisonerId = (SELECT PrisonerId FROM Admissions WHERE AdmissionId = " + computerNo + ")";

            // Save user and his relative FMD into database
            int count = HelperFunctions.ConnectDBnExecuteScript(saveFmdScript);

            if (count == 1)
            {
                PostFingerprintImage(pb3.Image, computerNo);
                MessageBox.Show("Successfully enrolled fingerprint of Computer Number : " + computerNo);
                Reset();
            }
            else
                MessageBox.Show("Computer Number " + computerNo + " NOT found!");

            //if (leftIndex == null || rightIndex == null) { MessageBox.Show("Fingerprint enrollment incomplete."); return; }
            //try
            //{
            //    identification.AddUser(new User(tbName.Text, leftIndex, rightIndex));
            //}
            //catch (ArgumentException ex)
            //{
            //    MessageBox.Show("User ID already taken.  Please enter a different ID.");
            //    return;
            //}
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //reader.Dispose();
            //this.Close();
            //return;
        }

        private void PostFingerprintImage(Image image, string computerNumber)
        {
            var client = new HttpClient();
            var content = new StreamContent(image.ToStream(ImageFormat.Png));

            var url = string.Format("http://{0}/PMIS/Prisoner/Fingerprint/{1}", _serverIP, computerNumber);

            var response = client.PostAsync(url, content);
            var result = response.Result.Content.ReadAsStringAsync().Result;

            if (!string.IsNullOrEmpty(result))
                MessageBox.Show("Fingerprint Photo not saved! Try again!");
        }

        private void Reset()
        {
            tbName.Text = "";
            pressCount = 0;
            pb1.Image = null; pb2.Image = null; pb3.Image = null; pb4.Image = null;
            fmd1 = null;
            btnSave.Enabled = false;
            listPreRegFMDs.Clear();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
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

            reader.On_Captured += new Reader.CaptureCallback(reader_On_Captured);
            reader.CaptureAsync(Constants.Formats.Fid.ISO, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, reader.Capabilities.Resolutions[0]);

            rbLeftIndex.Checked = true;
            lblInfo.Text = "Press your thumb 4 or more times.";
        }

        private void RegisterDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (reader != null)
            {
                reader.CancelCapture();
                reader.Dispose();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.' 
                && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
    }
}
