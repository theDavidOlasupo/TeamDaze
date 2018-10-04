﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SourceAFIS.Templates;
using SourceAFIS.General;
using SourceAFIS.Simple;
using System.Drawing.Imaging;
using Neurotec.Biometrics;

namespace TeamDaze.Web.Pages
{
    internal class EnrollmentResult
    {
        public NffvStatus engineStatus;
        public NffvUser engineUser;
    };


    public partial class SubmitPayment : System.Web.UI.Page
    {
        Nffv _engine;
        
        static AfisEngine Afis = new AfisEngine();
        string dbName = "FingerprintDB.CSharpSample.dat";
        string password = "";
        string scanner = "Nitgen";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                radio1.Items.Add(new ListItem("RT", "1"));
                radio1.Items.Add(new ListItem("RI", "2"));
                clearField();
                _engine = new Nffv(dbName, password);


            }
            else
            {
            }


        }

        public void btn_Click(object sender, EventArgs a)
        {
            //try
            //{
            //    //get the thumb print
            //    //chose the finger type
            //    //get the bvn and sedn to nibs
            //    //if it's a "00"
            //    //get the bvn details ...Enroll of it does not exist
            //    //ask for anxious finger
            //    var fingerChosen = radio1.SelectedValue.ToString();
            //    string bvn = txtBvn.ToString();
            //    string Amount = txtAmount.ToString();

            //    EnrollmentResult enrollmentResults = new EnrollmentResult();
            //    enrollmentResults.engineUser = _engine.Enroll(20000, out enrollmentResults.engineStatus);
            //   // args.Result = enrollmentResults;
            //    if (enrollmentResults.engineStatus == NffvStatus.TemplateCreated)
            //    {
            //        //it took a snap shot

            //        var img = enrollmentResults.engineUser.GetBitmap();
            //        var base64 = ConvertImageToBAse64(img);

            //        //call bvn service with temeplate.
            //        //if 00 ..check that bvn doesn't exist on db as user or panic figer
            //        //if panic finger..disable user
            //        //if does not exist..ask for panic finger and enroll

            //        //tokenize card on registration
            //        //pay by calling cards api and then ..pass the token
            //        return;  
            //        //convert the image to base 64 
            //    }
            //    else
            //    {
            //        return ;
            //    }

            //}
            //catch(Exception ex)
            //{

            //}
        }
        public void clearField()
        {
            txtAmount.Text = "";
            txtBvn.Text = "";
            //txtBvn.Text = "";
            // txtEmail.Text = "";
            //txtName.Text = "";
            // txtPhone.Text = "";

        }
        public string doEnroll(object sender, DoWorkEventArgs args)
        {
            EnrollmentResult enrollmentResults = new EnrollmentResult();
            enrollmentResults.engineUser = _engine.Enroll(20000, out enrollmentResults.engineStatus);
            args.Result = enrollmentResults;
            if (enrollmentResults.engineStatus == NffvStatus.TemplateCreated)
            {
                //it took a snap shot

                var img = enrollmentResults.engineUser.GetBitmap();
                var base64 = ConvertImageToBAse64(img);
                return base64;
                //convert the image to base 64 
            }
            else
            {
                return "99";
            }
        }
        public string ConvertImageToBAse64(Bitmap bitmapImage)
        {
            MemoryStream ms = new MemoryStream();

            bitmapImage.Save(ms, ImageFormat.Png);
            string Base64Image = Convert.ToBase64String(ms.ToArray());
            Fingerprint fp2 = new Fingerprint();
            //
            fp2.AsBitmap = new Bitmap(bitmapImage);

            Person person = new Person();
            person.Fingerprints.Add(fp2);

            //  fp2.AsIsoTemplate 
            Afis.Extract(person);
            var imgArray = fp2.Template;
            var isoTemplate = fp2.AsIsoTemplate;
            var isoImage = Convert.ToBase64String(isoTemplate);
            var ImagByteArray = Convert.ToBase64String(imgArray);
            ms.Position = 0;

            //BinaryWriter a = new BinaryWriter(File.Open("iso19794-2 template from fp_image1.dat", FileMode.Create));
            //foreach (byte element in isoTemplate)
            //{
            //    a.Write(element);
            //}


            return ImagByteArray;
            //return Base64Image;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //get the thumb print
                //chose the finger type
                //get the bvn and sedn to nibs
                //if it's a "00"
                //get the bvn details ...Enroll of it does not exist
                //ask for anxious finger
                var fingerChosen = radio1.SelectedValue.ToString();
                string bvn = txtBvn.ToString();
                string Amount = txtAmount.ToString();

                EnrollmentResult enrollmentResults = new EnrollmentResult();
                enrollmentResults.engineUser = _engine.Enroll(20000, out enrollmentResults.engineStatus);
                // args.Result = enrollmentResults;
                if (enrollmentResults.engineStatus == NffvStatus.TemplateCreated)
                {
                    //it took a snap shot

                    var img = enrollmentResults.engineUser.GetBitmap();
                    var base64 = ConvertImageToBAse64(img);

                    //call bvn service with temeplate.
                    //if 00 ..check that bvn doesn't exist on db as user or panic figer
                    //if panic finger..disable user
                    //if does not exist..ask for panic finger and enroll

                    //tokenize card on registration
                    //pay by calling cards api and then ..pass the token
                    return;
                    //convert the image to base 64 
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {

            }
        }
    }




}