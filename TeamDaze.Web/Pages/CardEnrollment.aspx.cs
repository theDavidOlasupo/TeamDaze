using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamDaze.Api.Utilities;

namespace TeamDaze.Web.Pages
{
    public partial class CardEnrollment : System.Web.UI.Page
    {
        string BaseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnVerifyBVN_Click(object sender, EventArgs e)
        {
            formone.Visible = false;
            formtwo.Visible = true;
        }

        protected async void btnValidateOtp_Click(object sender, EventArgs e)
        {
        }
    }
}