using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TeamDaze.BLL.DTO;

namespace TeamDaze.BLL.DAL
{
    public class TransactionRepository
    {
        //insert trxn
        //get trxn
        //mail trxn
        private static string ConString = ConfigurationManager.AppSettings["DbCon"];
        public void LogTrxn(string CustomerId, string FromAccount, string ToAccount, int MerchantId, string Amount, string Status)
        {
            try
            {
                string sql = "insert into Transaction(CustomerId,FromAccount, ToAccount, MerchantId, Amount, Status) values (@CustomerId,@FromAccount, @ToAccount,@MerchantId, @Amount, @Status) ";
                MSQconn c = new MSQconn(ConString);
                c.SetSQL(sql);
                c.AddParam("@CustomerId", CustomerId);
                c.AddParam("@FromAccount", FromAccount);
                c.AddParam("@ToAccount", ToAccount);
                c.AddParam("@MerchantId", MerchantId);
                c.AddParam("@Amount", Convert.ToDecimal(Amount));
                c.AddParam("@Amount", Amount);
                c.AddParam("@Status", Status);
                var resp = c.Insert();
                int respo = Convert.ToInt32(resp);
            }
            catch (Exception ex)
            {
                new ErrorLog(ex.ToString());
                //throw;
            }

        }

        public List<BLL.DTO.TransactionDto> GetCustomerTransactions(int CustomerId, DateTime ToDate, DateTime Fromdate, int isCust)
        {
            try
            {
                string sql = "select CustomerId,FromAccount, ToAccount, MerchantId, Amount from Transaction where CustomerId=@CustomerId and Status=@Status and CONVERT(DATE, CreatedOn) BETWEEN @FromDate AND @ToDate  order by CreatedOn desc";
                MSQconn c = new MSQconn(ConString);
                c.SetSQL(sql);
                c.AddParam("@CustomerId", CustomerId);
                c.AddParam("@FromDate", Fromdate);
                c.AddParam("@ToDate", ToDate);
                c.AddParam("@Status", 1);
                //for customer Status is  1
                //for merchant Status is  0
                var ds = c.Select();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = Helper.ConvertDataTable<BLL.DTO.TransactionDto>(ds.Tables[0]);
                    return list;
                }
                else
                {
                    // new ErrorLog("Get Customer error: " + ex.ToString());
                    List<BLL.DTO.TransactionDto> lt = new List<BLL.DTO.TransactionDto>();
                    return lt;
                }

            }
            catch (Exception ex)
            {
                new ErrorLog("Get trxn Customer error: " + ex.ToString());
                List<BLL.DTO.TransactionDto> lt = new List<BLL.DTO.TransactionDto>();
                return lt;
                //throw;
            }




        }


        public List<BLL.DTO.TransactionDto> GetMerchantTransactions(Int64 MerchantId, DateTime ToDate, DateTime Fromdate, int isCust)
        {
            try
            {
                string sql = "select Id CustomerId,FromAccount, ToAccount, MerchantId, Amount, CreatedOn from [Transaction] where MerchantId=@MerchantId and CONVERT(DATE, CreatedOn) BETWEEN @FromDate AND @ToDate order by CreatedOn desc";

                MSQconn c = new MSQconn(ConString);
                c.SetSQL(sql);
                c.AddParam("@MerchantId", MerchantId);
                c.AddParam("@FromDate", Fromdate);
                c.AddParam("@ToDate", ToDate);
                c.AddParam("@Status", 0);
                //for customer Status is 1
                //for merchant Status is 0
                var ds = c.Select();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var list = Helper.ConvertDataTable<BLL.DTO.TransactionDto>(ds.Tables[0]);
                        return list;
                    }
                    else
                        return null;
                }
                else
                {
                    // new ErrorLog("Get Customer error: " + ex.ToString());
                    List<BLL.DTO.TransactionDto> lt = new List<BLL.DTO.TransactionDto>();
                    return lt;
                }

            }
            catch (Exception ex)
            {
                new ErrorLog("Get trxn Customer error: " + ex.ToString());
                List<BLL.DTO.TransactionDto> lt = new List<BLL.DTO.TransactionDto>();
                return lt;
                //throw;
            }

        }
    }
}