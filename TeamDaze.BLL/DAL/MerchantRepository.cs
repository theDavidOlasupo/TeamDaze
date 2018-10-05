using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using TeamDaze.BLL.DTO;

namespace TeamDaze.BLL.DAL
{
    public class MerchantRepository
    {
        //create merchant insert
        //delete merchant
        //login merchant
        //get merchant acct
        private static string ConString = ConfigurationManager.AppSettings["DbCon"];

        public int CreateMerchant(MerchantCreation merchant)
        {
            //validate duplicate
            //insert merchant
            try
            {
                bool duplicated = ValidateDuplicateEnrollment(merchant);
                if (duplicated)
                {
                    return 99; //duplicate exists code
                }
                MSQconn con = new MSQconn(ConString);
                string sql = "insert into Merchant (Name,SettlementAccount,PhoneNumber,EmailAddress,Username,Password,CreatedOn,CreatedBy,Status) values (@Name,@SettlementAccount,@PhoneNumber,@EmailAddress,@Username,@Password,@CreatedOn,@CreatedBy,@Status)";
                con.SetSQL(sql);
                merchant.Password = Helper.Encrypt(merchant.Password);
                con.AddParam("@Name", merchant.Name);
                con.AddParam("@SettlementAccount", merchant.SettlementAccount);
                con.AddParam("@PhoneNumber", merchant.PhoneNumber);
                con.AddParam("@EmailAddress", merchant.EmailAddress);
                con.AddParam("@Username", merchant.Username);
                con.AddParam("@Password", merchant.Password);
                con.AddParam("@CreatedOn", merchant.CreatedOn);
                con.AddParam("@Status", merchant.Status);
                var resp = con.Insert();
                int r = Convert.ToInt16(resp);
                return r;
            }
            catch (Exception ex)
            {
                new ErrorLog("merchant creation error:" + ex.ToString());
                return 99; //error occored
            }


        }

        public Tuple<bool, List<MerchantCreation>> loginMerchant(string username, string password)
        {
            string sql = "select * from Merchant where Password=@Password and Username=@Username and Status=@Status";
            try
            {
                MSQconn con = new MSQconn(ConString);
                con.SetSQL(sql);
                password = Helper.Encrypt(password);
                con.AddParam("@Username", username);
                con.AddParam("@Password", password);
                con.AddParam("@Status", 1);
                DataSet ds = con.Select();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //var list = ds.Tables[0].Select().ToArray();
                    var list = Helper.ConvertDataTable<MerchantCreation>(ds.Tables[0]);
                    return Tuple.Create(true, list);
                }
                else
                {
                    List<MerchantCreation> lt = new List<MerchantCreation>();
                    return Tuple.Create(false, lt);
                    //DataRow[] lt =null;
                    //return Tuple.Create(true, lt);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog("Login merchant error: " + ex.ToString());
                List<MerchantCreation> lt = new List<MerchantCreation>();
                return Tuple.Create(false, lt);
            }
        }

        public Tuple<bool, List<MerchantCreation>> GetmerchantAcct(string PhoneNumber)
        {
            string sql = "select * from Merchant where Username=@Username and Status=@Status";
            try
            {
                MSQconn con = new MSQconn(ConString);
                con.SetSQL(sql);
                con.AddParam("@PhoneNumber", PhoneNumber);
                con.AddParam("@Status", 1);
                DataSet ds = con.Select();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //var list = ds.Tables[0].Select().ToArray();
                    var list = Helper.ConvertDataTable<MerchantCreation>(ds.Tables[0]);
                    return Tuple.Create(true, list);
                }
                else
                {
                    List<MerchantCreation> lt = new List<MerchantCreation>();
                    return Tuple.Create(false, lt);
                    //DataRow[] lt =null;
                    //return Tuple.Create(true, lt);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog("Login merchant error: " + ex.ToString());
                List<MerchantCreation> lt = new List<MerchantCreation>();
                return Tuple.Create(false, lt);
            }
        }
        public bool ValidateDuplicateEnrollment(MerchantCreation r)
        {
            try
            {
                MSQconn con = new MSQconn(ConString);
                string sql = "Select * from Merchant where PhoneNumber=@PhoneNumber or SettlementAccount=@SettlementAccount or EmailAddress=@EmailAddress";
                con.SetSQL(sql);
                // Dbcon.AddParam("@bvn", r.bvn);
                con.AddParam("@PhoneNumber", r.PhoneNumber);
                con.AddParam("@SettlementAccount", r.SettlementAccount);
                con.AddParam("@EmailAddress", r.EmailAddress);
                // Dbcon.AddParam("@HandleUsername", r.HandleUsername);
                DataSet ds = con.Select();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //duplicate
                    return true;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {
                new ErrorLog("ValidateDuplicateEnrollment error:" + ex.ToString());
                return false;
            }

        }
    }
}