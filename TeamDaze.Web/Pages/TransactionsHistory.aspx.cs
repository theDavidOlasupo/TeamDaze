using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamDaze.Api.Utilities;
using TeamDaze.BLL.DTO;

namespace TeamDaze.Web.Pages
{
    public partial class TransactionsHistory : System.Web.UI.Page
    {
        string BaseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        protected async void Page_Load(object sender, EventArgs e)
        {

            try
            {
                DateTime StartDate = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/01" );
                DateTime EndDate = DateTime.Now;

                var result = await GetTransactions(StartDate, EndDate);
                lstTransactionHistory.DataSource = result;
                lstTransactionHistory.DataBind();
                int TotalCount = result.Count;
                lblTotalSum.Text = result.Sum(c => c.Amount).ToString();
            }
            catch (Exception ex)
            {

            }
        }

        async Task<List<TransactionDto>> GetTransactions(DateTime fromDate, DateTime toDate)
        {
            string EndpointUrl = $"{BaseUrl}/api/Transactions/merchants?merchantid=1&fromDate={fromDate}&toDate={toDate}";

            var headers = new Dictionary<string, string>();
            var r = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(null, ApiRequest.Verbs.GET, headers);
            var responseString = await r.Content.ReadAsStringAsync();
            DefaultApiReponse<List<TransactionDto>> response = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<List<TransactionDto>>>(responseString);
            return response.Object;
        }

        protected async void btnView_Click(object sender, EventArgs e)
        {
            DateTime FromDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime ToDate = Convert.ToDateTime(txtStartDate.Text);
            var transactions = await GetTransactions(FromDate, ToDate);
            lstTransactionHistory.DataSource = transactions;
            lstTransactionHistory.DataBind();

        }
    }
}