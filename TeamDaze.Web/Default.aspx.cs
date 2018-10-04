using Neurotec.Biometrics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SourceAFIS.Templates;
using SourceAFIS.General;
using SourceAFIS.Simple;
namespace TeamDaze.Web
{
    internal class EnrollmentResult
    {
        public NffvStatus engineStatus;
        public NffvUser engineUser;
    };

    public partial class _Default : Page
    {
        Nffv _engine;
        static AfisEngine Afis = new AfisEngine();




        public _Default(Nffv engine)
        {
            _engine = engine;
        }
        internal class EnrollmentResult
        {
            public NffvStatus engineStatus;
            public NffvUser engineUser;
        };
        protected void Page_Load(object sender, EventArgs e)
        {

            string dbName = "FingerprintsDatabase.dat";
            string password = "passwd";
            Neurotec.Biometrics.Nffv engine = null;


            engine = new Neurotec.Biometrics.Nffv(dbName, password);

            //engine.Enroll(200000, Neurotec.Biometrics.NffvStatus)
            EnrollmentResult enrollmentResults = new EnrollmentResult();
            enrollmentResults.engineUser = _engine.Enroll(20000, out enrollmentResults.engineStatus);


            //var img = enrollmentResults.engineUser.GetBitmap();
            //convert to base 64 and call nibss
            engine.Cancel();

        }
        public void doEnroll(object sender, DoWorkEventArgs args)
        {
            EnrollmentResult enrollmentResults = new EnrollmentResult();
            enrollmentResults.engineUser = _engine.Enroll(20000, out enrollmentResults.engineStatus);
            args.Result = enrollmentResults;
            if (enrollmentResults.engineStatus == NffvStatus.TemplateCreated)
            {
                //it took a snap shot

                var img = enrollmentResults.engineUser.GetBitmap();
                var base64 = ConvertImageToBAse64(img);

                //convert the image to base 64 
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

    }
}