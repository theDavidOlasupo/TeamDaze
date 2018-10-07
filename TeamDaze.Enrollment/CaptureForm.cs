using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TeamDaze.BLL.DAL;

namespace Enrollment
{
    /* NOTE: This form is a base for the EnrollmentForm and the VerificationForm,
		All changes in the CaptureForm will be reflected in all its derived forms.
	*/
    public partial class CaptureForm : Form, DPFP.Capture.EventHandler
    {
        private IncidentRepository incidentRepository;
        private byte[] fingerPrint;
        public int ActionType;
        public CaptureForm()
        {
            InitializeComponent();
            incidentRepository = new IncidentRepository();
        }

        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if (null != Capturer)
                    Capturer.EventHandler = this;					// Subscribe for capturing events.
                else
                    SetPrompt("Can't initiate capture operation!");
            }
            catch
            {
                MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual void Process(DPFP.Sample Sample)
        {
            // Draw fingerprint sample image.
            var bitmap = ConvertSampleToBitmap(Sample);
            DrawPicture(bitmap);
            fingerPrint = ToByteArray(bitmap);
        }

        public byte[] ToByteArray(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Using the fingerprint reader, scan your fingerprint.");
                }
                catch
                {
                    SetPrompt("Can't initiate capture!");
                }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    SetPrompt("Can't terminate capture!");
                }
            }
        }

        #region Form Event Handlers:

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            Init();
            Start();                                                // Start capture operation.
            if(ActionType == 1)
            {
                lblActionDescription.Text = "Personnel Enrollment";
            }
            else
            {
                lblActionDescription.Text = "Head Count";
            }
        }

        private void CaptureForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }
        #endregion

        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("The fingerprint sample was captured.");
            //SetPrompt("Scan the same fingerprint again.");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
          //  MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            //MakeReport("The fingerprint reader was touched.");
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
        #endregion

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();  // Create a sample convertor.
            Bitmap bitmap = null;                                                           // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);                                 // TODO: return bitmap as a result
            return bitmap;
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        protected void SetStatus(string status)
        {
            this.Invoke(new Function(delegate ()
            {
                StatusLine.Text = status;
            }));
        }

        protected void SetPrompt(string prompt)
        {
            this.Invoke(new Function(delegate ()
            {
                Prompt.Text = prompt;
            }));
        }
        protected void MakeReport(string message)
        {
            this.Invoke(new Function(delegate ()
            {
                StatusText.AppendText(message + "\r\n");
            }));
        }

        private void DrawPicture(Bitmap bitmap)
        {
            this.Invoke(new Function(delegate ()
            {
                Picture.Image = new Bitmap(bitmap, Picture.Size);   // fit the image into the picture box
            }));
        }

        private DPFP.Capture.Capture Capturer;

        private void btnProfile_Click(object sender, EventArgs e)
        {
            long result = 0;
            try
            {
                if (fingerPrint == null || fingerPrint.Length == 0)
                {
                    MessageBox.Show("Please make sure fingerprint is taken before submitting form");
                    return;
                }

                if (txtBVN.Text != "")
                {
                    if (ActionType == 1)
                    {

                        result = incidentRepository.ProfileStaff(Convert.ToInt16(ConfigurationManager.AppSettings["CustomerId"]), txtBVN.Text);
                        if (result > 0)
                        {
                            MessageBox.Show("Personnel Registration was Successful");
                        }
                        else
                            MessageBox.Show("Please make sure all required fields are supplied");
                    }
                    else
                    {
                        result = incidentRepository.InsertIncident(Convert.ToInt16(ConfigurationManager.AppSettings["CustomerId"]), txtBVN.Text, "Insurgency");
                        if (result > 0)
                        {
                            MessageBox.Show("Personnel reported as safe");
                        }
                        else
                            MessageBox.Show("Please make sure all required fields are supplied");
                    }

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

    }
}