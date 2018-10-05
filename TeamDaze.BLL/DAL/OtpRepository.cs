using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using TeamDaze.BLL.DTO;

namespace TeamDaze.BLL.DAL
{
    public class OtpRepository
    {
        //insert trxn
        //get trxn
        //mail trxn
        private static string ConString = ConfigurationManager.AppSettings["DbCon"];
        public int Insert(string bvn, string otp)
        {
            int result = 0;
            try
            {
                string sql = "insert into Otp(Bvn,GeneratedOtp) values (@Bvn,@GeneratedOtp)";
                MSQconn c = new MSQconn(ConString);
                c.SetSQL(sql);                
                c.AddParam("@Bvn", bvn);
                c.AddParam("@GeneratedOtp", otp);
                result = Convert.ToInt32(c.Insert());
            }
            catch (Exception ex)
            {
                //new ErrorLog(ex.ToString());
                //throw;
            }
            return result;
        }

        public OtpResponse GetOtp(string Otp)
        {

            OtpResponse response = new OtpResponse();
            DataSet ds = null;
            try
            {
                string sql = "select * FROM Otp WHERE GeneratedOtp=@Otp";
                MSQconn c = new MSQconn(ConString);
                c.SetSQL(sql);
                c.AddParam("@Otp", Otp);
                ds = c.Select();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    response.Bvn = ds.Tables[0].Rows[0]["Bvn"].ToString();
                    response.Otp = ds.Tables[0].Rows[0]["GeneratedOtp"].ToString();
                    response.IsUsed = Convert.ToInt16(ds.Tables[0].Rows[0]["IsUsed"]);
                }
            }
            catch (Exception ex)
            {
                return response;
            }
            return response;
        }

        public int Update(string Otp)
        {
            int result = 0;
            try
            {
                string sql = "Update Otp SET IsUsed = 1 WHERE GeneratedOtp = @Otp)";
                MSQconn c = new MSQconn(ConString);
                c.AddParam("@Otp", Otp);
                var resp = c.Insert();
                 result = Convert.ToInt32(resp);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}