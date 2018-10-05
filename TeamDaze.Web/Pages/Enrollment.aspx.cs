using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamDaze.Api.Utilities;

namespace TeamDaze.Web.Pages
{
    public partial class Enrollment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnVerifyBVN_Click(object sender, EventArgs e)
        {

            //Rand 

            string Otp = "";
            string phoneNo = "";
            string EndpointUrl = $"http://api.smartsmssolutions.com/smsapi.php?username=DeeProg&password=Tremendous@1&sender=Laundry&recipient={phoneNo}&message={Otp}";

            var headers = new Dictionary<string, string>();
            var r = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(null, ApiRequest.Verbs.GET, headers);

            if(r.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await r.Content.ReadAsStringAsync();


            }
          

            //DefaultApiReponse<List<TransactionDto>> response = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<List<TransactionDto>>>(responseString);
            //return response.Object;
        }
    }
}