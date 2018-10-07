using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPFP;
using DPFP.Capture;
using DPFP.Gui;

namespace TeamDaze.Web.Pages
{
    public partial class FingerPrintTest : System.Web.UI.Page, DPFP.Gui.Enrollment.EventHandler
    {
        DPFP.Gui.Enrollment.EnrollmentControl EnrollmentControl;

        protected virtual void Init()
        {

            EventHandler = new DPFP.Gui.Enrollment.EnrollmentControl();             // Create a capture operation.


        }

        public void OnCancelEnroll(object Control, string ReaderSerialNumber, int Finger)
        {

        }

        public void OnComplete(object Control, string ReaderSerialNumber, int Finger)
        {
            throw new NotImplementedException();
        }

        public void OnDelete(object Control, int Finger, ref EventHandlerStatus EventHandlerStatus)
        {
            throw new NotImplementedException();
        }

        public void OnEnroll(object Control, int Finger, Template Template, ref EventHandlerStatus EventHandlerStatus)
        {
            Debug.WriteLine("Enrolled");
            Response.Write("<script language='javascript'>alert('Enrolled');</script>");
        }

        public void OnFingerRemove(object Control, string ReaderSerialNumber, int Finger)
        {
            throw new NotImplementedException();
        }

        public void OnFingerTouch(object Control, string ReaderSerialNumber, int Finger)
        {
            Debug.WriteLine("finger  touched");
            Response.Write("<script language='javascript'>alert('finger  touched');</script>");
        }

        public void OnReaderConnect(object Control, string ReaderSerialNumber, int Finger)
        {
            Debug.WriteLine("connected");
            Response.Write("<script language='javascript'>alert('connected');</script>");
        }

        public void OnReaderDisconnect(object Control, string ReaderSerialNumber, int Finger)
        {
            throw new NotImplementedException();
        }

        public void OnSampleQuality(object Control, string ReaderSerialNumber, int Finger, CaptureFeedback CaptureFeedback)
        {
            throw new NotImplementedException();
        }

        public void OnStartEnroll(object Control, string ReaderSerialNumber, int Finger)
        {
            throw new NotImplementedException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            


        }

        private DPFP.Capture.Capture Capturer;

        protected void SetPrompt(string prompt)
        {
            //this.Invoke(new Function(delegate () {
            //    Prompt.Text = prompt;
            //}));
            Debug.WriteLine(prompt);
        }

        protected void MakeReport(string message)
        {
            //this..BeginInvoke((Action)(() =>
            //{
            //    StatusText.AppendText(message + "\r\n");
            //}));
            Debug.WriteLine(message);
        }

        public DPFP.Gui.Enrollment.EnrollmentControl EventHandler;
    }
}