using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamDaze.Web.Pages
{
    public partial class SubmitPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

    }
}