using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamDaze.Web.Views
{
    public partial class MakePayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            //redirect to payment page
            Response.Redirect("SubmitPayment.aspx");
        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {

        }
    }
}