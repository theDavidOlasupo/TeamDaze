using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamDaze.Api;
namespace TeamDaze.Web.Pages
{
    public partial class InecReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //call the bvn search on the bvn..
            string bvn = txtBvn.Text;

            try
            {
                TeamDaze.Api.Controllers.SoapTester soapTester = new Api.Controllers.SoapTester();
               // var BvnDetails = soapTester.BvnSearch(bvn);
                if (BvnDetails != null)
                {
                    //send OTP to mail &number
                    if (SendOTP("", "") == 1)
                    {
                        Session["BVnDetails"] = BvnDetails;
                        Response.Redirect("ConfirmReg.aspx");
                        return;
                        //display detals on next screen
                    }
                //return img and parse picture
                }
                else
                {
                    //set the lbl to say so
                    lblGotBVn.Text = "Unable To Get BVN Details";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblGotBVn.Text = "Unable To Get BVN Details";
                //log exception
                return;
                //throw;
            }
            ///if it return
            //autopopulate the form
            //generate Guid and tvc
            //store in db

        }
        public System.Drawing.Bitmap Base64StringToBitmap(string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }

        public int SendOTP(string email, string phone)
        {

            try
            {
                //send & mail otp
                return 1;

            }
            catch(Exception ex)
            {
                return 0;
            }
        }
    }

}