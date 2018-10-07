using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Interop;
//using System.Windows.Media.Imaging;
using TeamDaze.BLL.DAL;
using SourceAFIS.Simple;
using System.Configuration;

namespace TeamDaze.IncidentSystem
{
    public partial class CaptureSafe : Form, DPFP.Capture.EventHandler
    {
        private IncidentRepository incidentRepository;
        private byte[] fingerPrint;

        public CaptureSafe()
        {
            InitializeComponent();
            incidentRepository = new IncidentRepository();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void CaptureSafe_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
            MainForm mainForm = (MainForm)Application.OpenForms["MainForm"];
            mainForm.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (fingerPrint == null || fingerPrint.Length == 0)
                {
                    MessageBox.Show("Please make sure fingerprint is taken before submitting form");
                    return;
                }
                
                if (txtBVN.Text != "")
                {
                    long studentId = incidentRepository.ProfileStaff(Convert.ToInt16( ConfigurationManager.AppSettings["CustomerId"]), txtBVN.Text);
                    if (studentId > 0)
                    {
                        MessageBox.Show("Staff Registration Successful");
                    }
                    else
                        MessageBox.Show("Please make sure all required fields are supplied");
                }
                else
                    MessageBox.Show("Please make sure all required fields are supplied");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearControls()
        {
            var controls = this.Controls;
            foreach (var control in controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    TextBox txt = control as TextBox;
                    txt.Clear();
                }
                if (control.GetType() == typeof(ComboBox))
                {
                    ComboBox cmb = control as ComboBox;
                    cmb.SelectedIndex = -1;
                }
            }
            //picLeftThumb.Image = Properties.Resources.biometric_ss1;
            //picRightThumb.Image = Properties.Resources.biometric_ss1;
            StatusText.Clear();
            StatusText.Visible = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //picRightThumb.Image = Properties.Resources.biometric_ss1;
            StatusText.Visible = false;
            StatusText.Clear();
        }

       

        private void CaptureSafe_Load(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(() => LoadDataSource());
            Task.Factory.StartNew(() => PopulateTemplateDatabase());
        }

        private void PopulateTemplateDatabase()
        {
            //var studentTemplates = registrationDAO.GetAllStudentsFingerTemplates();
            //Afis = new AfisEngine();
            //if (studentTemplates.Any())
            //{
            //    //enroll students
            //    foreach (var template in studentTemplates)
            //    {
            //        database.Add(Enroll(template.FingerPrint, template.MatricNo));
            //    }
            //}
            Init();
            //this.BeginInvoke((Action)(() =>
            //{

            //    btnAcquire.Enabled = true;
            //    btnReset.Enabled = true;
            //}));
        }

     
        private DPFP.Capture.Capture Capturer;
        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if (null != Capturer)
                    Capturer.EventHandler = this; // Subscribe for capturing events.
                else
                {
                    this.BeginInvoke((Action)(() =>
                    {
                        MessageBox.Show("Could not initiate fingerprint capture operation! \n" +
                                        "Check if the device is pluggedin or the driver has been installed");
                    }));
                }

            }
            catch
            {
                MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual Bitmap Process(DPFP.Sample Sample)
        {
            var bitmap = ConvertSampleToBitmap(Sample);
            fingerPrint = ToByteArray(bitmap);
            // Draw fingerprint sample image.
            DrawPicture(bitmap);
            return bitmap;
        }

        protected void Start()
        {
            try
            {
                if (Capturer != null)
                    Capturer.StartCapture();
            }
            catch
            {
            }
        }

        protected void Stop()
        {
            try
            {
                if (Capturer != null)
                    Capturer.StopCapture();
            }
            catch
            {
            }
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();	// Create a sample convertor.
            Bitmap bitmap = null;												            // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);									// TODO: return bitmap as a result
            return bitmap;
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();	// Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);			// TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        private void DrawPicture(Bitmap bitmap)
        {
            this.BeginInvoke((Action)(() =>
            {
                picRightThumb.Image = new Bitmap(bitmap, picRightThumb.Size);	// fit the image into the picture box
            }));
        }

        public byte[] ToByteArray(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }


        private void btnAcquire_Click(object sender, EventArgs e)
        {
            Start();
            StatusText.Visible = true;
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("The fingerprint sample was captured.");
            Process(Sample);
            //Afis.Threshold = 10;
            Random random = new Random();
            //RollCallForm.HostelStudent probe = Enroll(fingerPrint, "#" + random.Next(1000, 9999));
            //RollCallForm.HostelStudent match = Afis.Identify(probe, database).FirstOrDefault() as RollCallForm.HostelStudent;
            // Null result means that there is no candidate with similarity score above threshold
            if (true) //match
            {
                this.BeginInvoke((Action)(() =>
                {
                   // btnRegister.Enabled = false;
                    MessageBox.Show("Double enrollment of fingerprint is not allowed", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }));
            }
            else
            {
                this.BeginInvoke((Action)(() =>
                {
                    //btnRegister.Enabled = true;
                }));
            }

        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was touched.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("The quality of the fingerprint sample is good.");
            else
                MakeReport("The quality of the fingerprint sample is poor.");
        }

        protected void MakeReport(string message)
        {
            this.BeginInvoke((Action)(() =>
            {
                StatusText.AppendText(message + "\r\n");
            }));
        }

        private void CaptureSafe_Load_1(object sender, EventArgs e)
        {

        }


        //protected virtual void Init()
        //{
        //    try
        //    {
        //        Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

        //        if (null != Capturer)
        //            Capturer.EventHandler = this;					// Subscribe for capturing events.
        //        else
        //            SetPrompt("Can't initiate capture operation!");

        //        Enroller = new DPFP.Processing.Enrollment();            // Create an enrollment.
        //        UpdateStatus();
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //protected virtual void Process(DPFP.Sample Sample)
        //{
        //    // Draw fingerprint sample image.
        //    DrawPicture(ConvertSampleToBitmap(Sample));
        //}

        //protected void Start()
        //{
        //    if (null != Capturer)
        //    {
        //        try
        //        {
        //            Capturer.StartCapture();
        //            SetPrompt("Using the fingerprint reader, scan your fingerprint.");
        //        }
        //        catch
        //        {
        //            SetPrompt("Can't initiate capture!");
        //        }
        //    }
        //}

        //protected void Stop()
        //{
        //    if (null != Capturer)
        //    {
        //        try
        //        {
        //            Capturer.StopCapture();
        //        }
        //        catch
        //        {
        //            SetPrompt("Can't terminate capture!");
        //        }
        //    }
        //}

        //#region Form Event Handlers:

        //private void CaptureForm_Load(object sender, EventArgs e)
        //{
        //    Init();
        //    Start();                                                // Start capture operation.
        //}

        //private void CaptureForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Stop();
        //}
        //#endregion

        //#region EventHandler Members:

        //public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        //{
        //    MakeReport("The fingerprint sample was captured.");
        //    SetPrompt("Scan the same fingerprint again.");
        //    Process(Sample);
        //}

        //public void OnFingerGone(object Capture, string ReaderSerialNumber)
        //{
        //    MakeReport("The finger was removed from the fingerprint reader.");
        //}

        //public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        //{
        //    MakeReport("The fingerprint reader was touched.");
        //}

        //public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        //{
        //    MakeReport("The fingerprint reader was connected.");
        //}

        //public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        //{
        //    MakeReport("The fingerprint reader was disconnected.");
        //}

        //public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        //{
        //    if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
        //        MakeReport("The quality of the fingerprint sample is good.");
        //    else
        //        MakeReport("The quality of the fingerprint sample is poor.");
        //}
        //#endregion

        //protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        //{
        //    DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();  // Create a sample convertor.
        //    Bitmap bitmap = null;                                                           // TODO: the size doesn't matter
        //    Convertor.ConvertToPicture(Sample, ref bitmap);                                 // TODO: return bitmap as a result
        //    return bitmap;
        //}

        //protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        //{
        //    DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
        //    DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
        //    DPFP.FeatureSet features = new DPFP.FeatureSet();
        //    Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
        //    if (feedback == DPFP.Capture.CaptureFeedback.Good)
        //        return features;
        //    else
        //        return null;
        //}

        //protected void SetStatus(string status)
        //{
        //    this.BeginInvoke((Action)(() =>
        //    {
        //        StatusLine.Text = status;
        //    }));
        //}

        //protected void SetPrompt(string prompt)
        //{
        //    this.BeginInvoke((Action)(() =>
        //    {
        //        Prompt.Text = prompt;
        //    }));
        //}
        //protected void MakeReport(string message)
        //{
        //    this.BeginInvoke((Action)(() =>
        //    {
        //        StatusText.AppendText(message + "\r\n");
        //    }));


        //}

        //private void DrawPicture(Bitmap bitmap)
        //{
        //    this.BeginInvoke((Action)(() =>
        //    {
        //        Picture.Image = new Bitmap(bitmap, Picture.Size);   // fit the image into the picture box
        //    }));
        //}
        //private void UpdateStatus()
        //{
        //    // Show number of samples needed.
        //    SetStatus(String.Format("Fingerprint samples needed: {0}", Enroller.FeaturesNeeded));
        //}

        //private DPFP.Capture.Capture Capturer;
        //private DPFP.Processing.Enrollment Enroller;
    }
}
