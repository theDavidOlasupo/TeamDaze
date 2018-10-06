using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TeamDaze.BLL.DAL;
using TeamDaze.BLL.DTO;

namespace TeamDaze.Api.Controllers
{
    [RoutePrefix("api/Otp")]
    public class OtpController : ApiControllerBase
    {
        OtpRepository otpRepository = new OtpRepository();

        [HttpPost]
        [Route("")]
        public async Task<IApiResponse<int>> Insert(OtpRequest request)
        {
            int result = otpRepository.Insert(request.Bvn, request.Otp);

            return await HandleApiOperationAsync(async () =>
            {
                return new DefaultApiReponse<int>
                {
                    Object = result
                };
            });
        }

        [HttpPost]
        [Route("Validate")]
        public async Task<IApiResponse<int>> Update(OtpRequest request)
        {
            int result = otpRepository.Update(request.Otp);

            string message = result != 1 ? "Invalid OTP" : "SUCCESS";

            return await HandleApiOperationAsync(async () =>
            {
                return new DefaultApiReponse<int>
                {
                    Object = result,
                    ShortDescription = message
                };
            });
        }

        [HttpGet]
        [Route("")]
        public async Task<IApiResponse<OtpResponse>> Get(string otp)
        {
            var result = otpRepository.GetOtp(otp);

            return await HandleApiOperationAsync(async () =>
            {
                return new DefaultApiReponse<OtpResponse>
                {
                    Object = result
                };
            });
        }
    }
}
