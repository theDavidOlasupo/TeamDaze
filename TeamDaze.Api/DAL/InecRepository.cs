using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using TeamDaze.Api.DTO;

namespace TeamDaze.Api.DAL
{
    public class InecRepository
    {
        private static string ConString = ConfigurationManager.AppSettings["DbCon"];
        public void LogInecRegistration(BvnSearchResp RegDetails)
        {
            try
            {
                string sql = "insert into InecReg (BVN,RegistrationDate, FirstName, MiddleName, DateOfBirth, Email,Gender,LgaOfOrigin,LgaOfResidence,ResidentialAddress,StateOfOrigin,StateOfResidence,Base64Image) values (@BVN,@RegistrationDate, @FirstName, @MiddleName, @DateOfBirth, @Email,@Gender,@LgaOfOrigin,@LgaOfResidence,@ResidentialAddress,@StateOfOrigin,@StateOfResidence,@Base64Image) ";
                MSQconn c = new MSQconn(ConString);
                c.SetSQL(sql);
                c.AddParam("@BVN", RegDetails.BVN);
                c.AddParam("@RegistrationDate", RegDetails.RegistrationDate);
                c.AddParam("@FirstName", RegDetails.FirstName);
                c.AddParam("@MiddleName", RegDetails.MiddleName);
                c.AddParam("@DateOfBirth", RegDetails.DateOfBirth);
                c.AddParam("@Email", RegDetails.Email);
                c.AddParam("@Gender", RegDetails.Gender);
                c.AddParam("@LgaOfOrigin", RegDetails.LgaOfOrigin);
                c.AddParam("@LgaOfResidence", RegDetails.LgaOfResidence);
                c.AddParam("@ResidentialAddress", RegDetails.ResidentialAddress);
                c.AddParam("@StateOfOrigin", RegDetails.StateOfOrigin);
                c.AddParam("@StateOfResidence", RegDetails.StateOfResidence);
                c.AddParam("@Base64Image", RegDetails.Base64Image);
                var resp = c.Insert();
                int respo = Convert.ToInt32(resp);
            }
            catch (Exception ex)
            {
                new ErrorLog(ex.ToString());
                //throw;
            }

        }


        public Tuple<bool, List<BvnSearchResp>> GetVoters(string bvn = "")
        {
            try
            {
                MSQconn con = new MSQconn(ConString);
                string sql = "";
                if (bvn == "")
                {
                    sql = @"SELECT *,[BVN],[RegistrationDate],[FirstName],[MiddleName],[DateOfBirth],[Email],[Gender],[LgaOfOrigin],[LgaOfResidence],[ResidentialAddress],[StateOfOrigin],[StateOfResidence],[Base64Image] FROM InecReg";
                }
                else
                {
                    sql = "select * from InecReg where BVN=@BVN order by desc ";
                }

                con.SetSQL(sql);
                con.AddParam("@BVN", bvn);
                //  con.AddParam("@Status", 1);
                DataSet ds = con.Select();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = Helper.ConvertDataTable<BvnSearchResp>(ds.Tables[0]);
                    return Tuple.Create(true, list);
                }
                else
                {
                    List<BvnSearchResp> lt = new List<BvnSearchResp>();
                    return Tuple.Create(false, lt);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(ex);
                List<BvnSearchResp> lt = new List<BvnSearchResp>();
                return Tuple.Create(false, lt);
            }
        }
    }
}