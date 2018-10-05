using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TeamDaze.BLL.DAL;
using TeamDaze.BLL.DTO;

namespace TeamDaze.Api.Controllers
{
    [RoutePrefix("api/Transactions")]
    public class TransactionsController : ApiControllerBase
    {
        TransactionRepository transactionRepository = new TransactionRepository();

        [HttpGet]
        [Route("merchants")]
        public async Task<IApiResponse<List<TransactionDto>>> GetTransactions(Int64 merchantId, DateTime fromDate, DateTime toDate)
        {
            var result = transactionRepository.GetMerchantTransactions(merchantId, toDate, fromDate, 1);


            return await HandleApiOperationAsync(async () =>
            {
                return new DefaultApiReponse<List<TransactionDto>>
                {
                    Object = result
                };
            });
        }
    }
}
