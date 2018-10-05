using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TeamDaze.Api.DAL;
using TeamDaze.Api.DTO;

namespace TeamDaze.Api.Controllers
{
    /// <summary>
    /// Summary description for SoapTester
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoapTester : System.Web.Services.WebService
    {
        [WebMethod]
        public string GenerateCredentials(string bvn)
        {
            NibssRepository restobj = new NibssRepository();
            string resp = restobj.GenerateCredentials(bvn);
            return resp;
        }
        [WebMethod]
        public MatchWithBvnResp MatchWithBvn(string Bvn, string Position, string ISOTemplate)
        {
            NibssRepository restobj = new NibssRepository();
            var resp = restobj.MatchWithBvn(Bvn, Position, ISOTemplate);
            return resp;
        }

        [WebMethod]
        public MatchWithBvnResp MatchWithPhone(string Phone, string Position, string ISOTemplate)
        {
            NibssRepository restobj = new NibssRepository();
            var resp = restobj.MatchWithPhonenumber(Phone, Position, ISOTemplate);
            return resp;
        }

        //[WebMethod]
        //public BvnSearchResp BvnSearch(string bvn)
        //{
        //    NibssRepository restobj = new NibssRepository();
        //    var resp = restobj.BvnSearch(bvn);
        //    return resp;
        //}
    }
}
