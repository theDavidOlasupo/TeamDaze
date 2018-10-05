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
    [RoutePrefix("api/nibss")]
    public class NibssController : ApiControllerBase
    {
        NibssRepository nibssRepository = new NibssRepository();

        [HttpGet]
        [Route("searchbvn")]
        public async Task<IApiResponse<BvnSearchResp>> BVNSearch(string bvn)
        {
            var result = nibssRepository.BvnSearch(bvn);

            return await HandleApiOperationAsync(async () =>
            {
                return new DefaultApiReponse<BvnSearchResp>
                {
                    Object = result
                };
            });
        }

    }
}
