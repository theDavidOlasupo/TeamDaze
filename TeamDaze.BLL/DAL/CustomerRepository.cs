using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using TeamDaze.BLL.DTO;

namespace TeamDaze.BLL.DAL
{
    public class CustomerRepository
    {
        //check if user exists and insert records
        //set anxious finger
        //insert trxns
        //mail receipt
        private static string ConString = ConfigurationManager.AppSettings["DbCon"];

        public Tuple<bool, List<CustomerCreation>> CreateCustomer(CustomerCreation Customer)
        {
            try
            {
                var duplicate = ValidateDuplicateEnrollment(Customer.BVN);
                if (duplicate)
                {
                    List<CustomerCreation> lt = new List<CustomerCreation>();
                    return Tuple.Create(false, lt);
                }
                MSQconn con = new MSQconn(ConString);
                if(Customer.MaxAmount < 0)
                {
                    Customer.MaxAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["DefaultMaxAmount"]);
                }
                Customer.CreatedBy = "TeamDaze Admin";
                string sql = @"insert into Customer (FirstName,LastName,BVN,PhoneNumber,EmailAddress,PanicFinger,CreatedBy,Status,
                            CreatedOn, EnrollmentType, CardType, CardToken) values (@FirstName,@LastName,@BVN,@PhoneNumber,@EmailAddress,@PanicFinger,@CreatedBy,@Status)";
                con.SetSQL(sql);
                con.AddParam("@FirstName", Customer.FirstName);
                con.AddParam("@LastName", Customer.LastName);
                con.AddParam("@BVN", Customer.BVN);
                con.AddParam("@PhoneNumber", Customer.PhoneNumber);
                con.AddParam("@EmailAddress", Customer.EmailAddress);
                con.AddParam("@PanicFinger", Customer.PanicFinger);
                con.AddParam("@CreatedBy", Customer.CreatedBy);
                con.AddParam("@MaxAmount", Customer.MaxAmount);
                con.AddParam("@Status", 1);
                con.AddParam("@CreatedOn", DateTime.Now);
                con.AddParam("@EnrollmentType", Customer.EnrollmentType);
                con.AddParam("@CardType", Customer.CardType);
                con.AddParam("@CardToken", Customer.CardToken);


                var resp = con.Insert();
                int r = Convert.ToInt16(resp);
                if(r == 1)
                {
                    List<CustomerCreation> lt = new List<CustomerCreation>();
                    lt.Add(Customer);
                    return Tuple.Create(true, lt);
                }
                else
                {
                    List<CustomerCreation> lt = new List<CustomerCreation>();
                    return Tuple.Create(false, lt);
                }

            }
            catch (Exception ex)
            {
                new ErrorLog("Customer creation error:" + ex.ToString());
                List<CustomerCreation> lt = new List<CustomerCreation>();
                return Tuple.Create(false, lt);
            }
        }


        public bool ValidateDuplicateEnrollment(string bvn)
        {
            try
            {
                MSQconn con = new MSQconn(ConString);
                string sql = "Select * from Customer where BVN=@BVN";
                con.SetSQL(sql);
                con.AddParam("@BVN", bvn);
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

        public Tuple<bool, List<CustomerCreation>> GetCustomer(string BVN)
        {
            string sql = "select * from Customer where BVN=@BVN and Status=@Status";
            try
            {
                MSQconn con = new MSQconn(ConString);
                con.SetSQL(sql);
                con.AddParam("@BVN", BVN);
                con.AddParam("@Status", 1);
                DataSet ds = con.Select();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //var list = ds.Tables[0].Select().ToArray();
                    var list = Helper.ConvertDataTable<CustomerCreation>(ds.Tables[0]);
                    return Tuple.Create(true, list);
                }
                else
                {
                    List<CustomerCreation> lt = new List<CustomerCreation>();
                    return Tuple.Create(false, lt);
                     
                }
            }
            catch (Exception ex)
            {
                new ErrorLog("Get customer details error: " + ex.ToString());
                List<CustomerCreation> lt = new List<CustomerCreation>();
                return Tuple.Create(false, lt);
            }
        }

        public bool SetPanicFinger(string PanicFingerPsoition, string BVN)
        {
            //update the panic finger
            string sql = "update Customer set PanicFinger=@PanicFinger where BVN =@BVN and  Status=1";
            MSQconn con = new MSQconn(ConString);
            con.SetSQL(sql);
            con.AddParam("@BVN", BVN);
            int resp =con.Update();
            if(resp == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}