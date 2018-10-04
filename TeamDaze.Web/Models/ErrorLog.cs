using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TeamDaze.Web.Models
{
     class ErrorLog
    {
        public ErrorLog(Exception ex)
        {
            //string pth = "G:\\Appslog\\ussdlog\\logs\\";
            // string pth = "G:\\Appslog\\ismlog\\logs\\";
            string pth = "C:\\Appslog\\icadlog\\logs\\";
            string err = ex.ToString();
            DateTime dt = DateTime.Now;
            string fld = dt.ToString("yyyy") + "_" + dt.ToString("MM") + "_";
            pth += fld + dt.ToString("dd") + ".txt";
            if (!File.Exists(pth))
            {
                using (StreamWriter sw = File.CreateText(pth))
                {
                    sw.WriteLine(dt.ToString() + " : " + err);
                    sw.WriteLine(" ");
                    sw.Close();
                    sw.Dispose();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(pth))
                {
                    sw.WriteLine(dt.ToString() + " : " + err);
                    sw.WriteLine(" ");
                    sw.Close();
                    sw.Dispose();
                }

            }

        }
        public ErrorLog(string ex)
        {
            // string pth = "G:\\Appslog\\ismlog\\logs\\";
            string pth = "C:\\Appslog\\";
            //string pth = "C:\\Appslog\\icadlog\\logs\\";
            string err = ex;
            DateTime dt = DateTime.Now;
            string fld = dt.ToString("yyyy") + "_" + dt.ToString("MM") + "_";
            pth += fld + dt.ToString("dd") + ".txt";
            if (!File.Exists(pth))
            {
                using (StreamWriter sw = File.CreateText(pth))
                {
                    sw.WriteLine(dt.ToString() + " : " + err);
                    sw.WriteLine(" ");
                    sw.Close();
                    sw.Dispose();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(pth))
                {
                    sw.WriteLine(dt.ToString() + " : " + err);
                    sw.WriteLine(" ");
                    sw.Close();
                    sw.Dispose();
                }

            }

        }
     
    }
}