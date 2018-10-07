using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDaze.BLL.DAL
{
   public class IncidentRepository
    {
        private static string ConString = ConfigurationManager.AppSettings["DbCon"];
        public long ProfileEmployer(string Name, string BVN, string Address, string Type)
        {
            int result = 0;
            try
            {
                MSQconn con = new MSQconn(ConString);
                string sql = @"insert into Employer (Name,BVN,Address,Type) values (@Name,@BVN,@Address,@Type)";
                con.SetSQL(sql);
                con.AddParam("@Name", Name);
                con.AddParam("@BVN", BVN);
                con.AddParam("@Address", Address);
                con.AddParam("@Type", Type);
                var resp = con.Insert();
               result = Convert.ToInt16(resp);
            }
            catch (Exception ex)
            {
                
                return 0;
            }

            return result;
        }

        public long ProfileStaff(int EmployerId, string BVN)
        {
            int result = 0;
            try
            {
                MSQconn con = new MSQconn(ConString);
                string sql = @"insert into Staff (EmployerId,BVN) values (@EmployerId,@BVN)";
                con.SetSQL(sql);
                con.AddParam("@EmployerId", EmployerId);
                con.AddParam("@BVN", BVN);
                var resp = con.Insert();
                result = Convert.ToInt16(resp);
            }
            catch (Exception ex)
            {

                return 0;
            }

            return result;
        }

        public long InsertIncident(int EmployerId, string BVN, string Type)
        {
            int result = 0;
            try
            {
                MSQconn con = new MSQconn(ConString);
                string sql = @"insert into Incident (EmployerId,BVN,Type) values (@EmployerId,@BVN,@Type)";
                con.SetSQL(sql);
                con.AddParam("@EmployerId", EmployerId);
                con.AddParam("@BVN", BVN);
                con.AddParam("@Type", Type);
                var resp = con.Insert();
                result = Convert.ToInt16(resp);
            }
            catch (Exception ex)
            {

                return 0;
            }

            return result;
        }

        //public DataSet GetIncident(int EmployerId, string Type, DateTime FromDate, DateTime ToDate)
            public DataSet GetIncident()
        {
            DataSet result = null;
            try
            {
                MSQconn con = new MSQconn(ConString);
                string sql = @"select * from [Incident]";
                con.SetSQL(sql);
                //con.AddParam("@EmployerId", EmployerId);
                //con.AddParam("@Type", Type);
                //con.AddParam("@FromDate", FromDate);
                //con.AddParam("@ToDate", ToDate);
                 result = con.Select();
            }
            catch (Exception ex)
            {
                return null;
            }

            return result;
        }
    }
}
