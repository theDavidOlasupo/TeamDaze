using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TeamDaze.Api.DTO;

namespace TeamDaze.Api.DAL
{
    class NibssRepository
    {

        public string GenerateCredentials(string bvn)
        {
            string NibssBaseUrl = ConfigurationManager.AppSettings["NibssBaseApiUrl"];
            string MethodUrl = "/GenerateCredentials";
            NibssBaseUrl += MethodUrl;
            RestClient client = new RestClient(NibssBaseUrl);
            //  ByPassProxy(client);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "text/plain");
            request.AddHeader("Content-Type", "application/json");

            var Base64Bvn = Helper.Base64Encode(bvn);
            request.AddHeader("UserCode", Base64Bvn);

            //execute the request.
            var response = client.Execute(request);

            //read the headers for the iv and AES keys and also the hash.
            //The request was successful and AES and IV keys and Hash were successfully generated and returned via AES_KEY, IVKey and HASH header parameters respectively.

            if (response.StatusCode.ToString() != null)
            {
                //log and read the headers
                try
                {
                    string resetresp = response.StatusDescription + "|" + response.StatusCode + "|" + response.Content;
                    string Aes_key = Convert.ToString(response.Headers.Where(x => x.Name == "AES_KEY").SingleOrDefault().Value);
                    string IVKey = Convert.ToString(response.Headers.Where(x => x.Name == "IVKey").SingleOrDefault().Value);
                    string HASH = Convert.ToString(response.Headers.Where(x => x.Name == "HASH").SingleOrDefault().Value);

                    var header = response.Headers;
                    foreach (var headerval in header)
                    {
                        new ErrorLog("Name: " + headerval.Name + " Value:" + headerval.Value.ToString());
                    }
                    string credentials = "AES KEY :" + Aes_key + "|| IV-KEY: " + IVKey + " || Hash: " + HASH;
                    new ErrorLog(resetresp + " Credentials: " + credentials);
                    return resetresp + " credentials:" + credentials;

                }
                catch (Exception ex)
                {
                    new ErrorLog(ex);

                    return "an error occurred";
                }
            }
            else
            {
                string resetresp = "no response";
                return resetresp;
            }

        }

        public MatchWithBvnResp MatchWithBvn(string Bvn, string Position, string ISOTemplate)
        {
            string NibssBaseUrl = ConfigurationManager.AppSettings["NibssBaseApiUrl"];
            string MethodUrl = "/MatchWithBVN";
            NibssBaseUrl += MethodUrl;
            RestClient client = new RestClient(NibssBaseUrl);
            //  ByPassProxy(client);
            var hash = ConfigurationManager.AppSettings["hackHASH"];
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Hash", hash);

            String key = ConfigurationManager.AppSettings["hackAES"]; //"OUGW5XNSc/82rXAr"; 
            String iv = ConfigurationManager.AppSettings["hackIVKey"];// "1BNEMKUJXi3svqFk"; 

            MatchWithBvnReq req = new MatchWithBvnReq
            {
                BVN = Bvn,
                Position = Position,
                ISOTemplate = ISOTemplate
            };


            var json = new JavaScriptSerializer().Serialize(req);

            //encrypt the request
            String encryptedRequest = AES.Encrypt(json.ToString(), key, iv);

            var requestBody = request.AddParameter("application/json", encryptedRequest, ParameterType.RequestBody);

            // String decryptedText = AES.Decrypt(encryptedRequest, key, iv);
            var response = client.Execute(request);

            //decrypt the response body

            var responseBody = response.Content.ToString();
            new ErrorLog(responseBody);



            string decryptedResponse = AES.Decrypt(responseBody, key, iv);
            //var decryptedResponse = AES.AesDecrypt(Convert.FromBase64String(responseBody), Encoding.UTF8.GetBytes(key), Convert.FromBase64String(iv));


            //            var payload = Encoding.UTF8.GetString(decryptedResponse, 0, decryptedResponse.Length);

            var resp = new JavaScriptSerializer().Deserialize<MatchWithBvnResp>(decryptedResponse);
            //log response
            new ErrorLog(resp.ToString() + "MatchWithBVN decrpted response: " + decryptedResponse);

            return resp;
        }

        public MatchWithBvnResp MatchWithPhonenumber(string PhoneNumber, string Position, string ISOTemplate)
        {
            string NibssBaseUrl = ConfigurationManager.AppSettings["NibssBaseApiUrl"];
            string MethodUrl = "/MatchWithPhoneNumber";
            NibssBaseUrl += MethodUrl;
            RestClient client = new RestClient(NibssBaseUrl);
            //  ByPassProxy(client);
            var hash = ConfigurationManager.AppSettings["hackHASH"];
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Hash", hash);

            String key = ConfigurationManager.AppSettings["hackAES"]; //"OUGW5XNSc/82rXAr"; 
            String iv = ConfigurationManager.AppSettings["hackIVKey"];// "1BNEMKUJXi3svqFk"; 
            MatchWithPhoneNumrReq req = new MatchWithPhoneNumrReq
            {
                PhoneNumber = PhoneNumber,
                Position = Position,
                ISOTemplate = ISOTemplate
            };

            var json = new JavaScriptSerializer().Serialize(req);

            //encrypt the request
            String encryptedRequest = AES.Encrypt(json.ToString(), key, iv);

            var requestBody = request.AddParameter("application/json", encryptedRequest, ParameterType.RequestBody);

            // String decryptedText = AES.Decrypt(encryptedRequest, key, iv);
            var response = client.Execute(request);

            //decrypt the response body
            var responseBody = response.Content.ToString();
            new ErrorLog(responseBody);
            string decryptedResponse = AES.Decrypt(responseBody, key, iv);
            var resp = new JavaScriptSerializer().Deserialize<MatchWithBvnResp>(decryptedResponse);
            //log response
            new ErrorLog(resp.ToString() + "MatchWithPhonenumber decrpted response: " + decryptedResponse);

            return resp;

        }

        public BLL.DTO.BvnSearchResp BvnSearch(string bvn)
        {
           BLL.DTO.BvnSearchResp bvndetails = new BLL.DTO.BvnSearchResp();

            try
            {
                string NibssBaseUrl = ConfigurationManager.AppSettings["NibssBaseApiUrl"];
                string MethodUrl = "/BVNSearch";
                NibssBaseUrl += MethodUrl;
                RestClient client = new RestClient(NibssBaseUrl);
                //  ByPassProxy(client);
                var hash = ConfigurationManager.AppSettings["hackHASH"];
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Hash", hash);

                String key = ConfigurationManager.AppSettings["hackAES"]; //"OUGW5XNSc/82rXAr"; 
                String iv = ConfigurationManager.AppSettings["hackIVKey"];// "1BNEMKUJXi3svqFk"; 
                BvnSearchReq req = new BvnSearchReq
                {
                    BVN = bvn
                };
                var json = new JavaScriptSerializer().Serialize(req);
                //encrypt the request
                String encryptedRequest = AES.Encrypt(json.ToString(), key, iv);
                var requestBody = request.AddParameter("application/json", encryptedRequest, ParameterType.RequestBody);
                // String decryptedText = AES.Decrypt(encryptedRequest, key, iv);
                var response = client.Execute(request);
                //decrypt the response body
                var responseBody = response.Content.ToString();
               // new ErrorLog(responseBody);
                string decryptedResponse = AES.Decrypt(responseBody, key, iv);
                bvndetails = new JavaScriptSerializer().Deserialize<BLL.DTO.BvnSearchResp>(decryptedResponse);
                //log response
                //new ErrorLog(bvndetails.ToString() + "bvn search decrpted response: " + decryptedResponse);

                return bvndetails;
            }
            catch (Exception ex)
            {
                new ErrorLog(ex.ToString());
                //  throw;
                return bvndetails;
            }

        }
    }
}