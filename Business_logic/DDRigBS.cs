using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DDRigMonitoring.Utility;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.WebPages;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using Newtonsoft.Json.Linq;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;
using System.Web.Services.Description;
using DDRigWeb.Models;
namespace DDRigWeb.Business_logic
{
    public class DDRigBS
    {
        private Database database;
        public DDRigBS()
        {
            database = new Database(Database.Source.REDBOW, Database.Catalog.Thailis);
        }

        public List<RigModel> GetAllRigs(String Line)
        {


            DataTable tmpTable = new DataTable();
            List<RigModel> result = new List<RigModel>();
            
            string queryStr;


            //queryStr = "select rtl.SerialNo1,rtl.SerialNo2, rtl.NameRig, Ltrim(RTRIM(rtl.StatBtn)) as 'StatBtn', rtl.CellNo,rcd.CellName , rtl.StopUpdate From DDRigTstLog rtl " +
            //           "inner join DDRigCellDesc rcd on rtl.CellNo = rcd.CellNo where rtl.NameRig ='" + Line + "' order by rtl.CellNo ";
            //            queryStr = @"
            //SELECT 
            //    rtl.SerialNo1,
            //    rtl.SerialNo2,
            //    rtl.NameRig,
            //    LTRIM(RTRIM(rtl.StatBtn)) AS StatBtn,
            //    rtl.CellNo,
            //    rcd.CellName,
            //    rtl.startUpdate,
            //    rtl.StopUpdate,
            //    CASE 
            //        WHEN rtl.StopUpdate < GETDATE() THEN 'OK'
            //        ELSE 'NG'
            //    END AS DateCheck
            //FROM DDRigTstLog rtl
            //INNER JOIN DDRigCellDesc rcd 
            //    ON rtl.CellNo = rcd.CellNo
            //WHERE rtl.NameRig = '" + Line + @"'
            //ORDER BY rtl.CellNo;
            //";
            queryStr = @"SELECT 
    rtl.SerialNo1,
    rtl.SerialNo2,
    rtl.NameRig,
    LTRIM(RTRIM(rtl.StatBtn)) AS StatBtn,
    rtl.CellNo,
    rcd.CellName,
    rtl.startUpdate,
    rtl.StopUpdate,
    CASE 
        WHEN rtl.StopUpdate < GETDATE() THEN 'OK'
        ELSE 'NG'
    END AS DateCheck,
     CASE 
        WHEN rtl.StopUpdate = (
            SELECT MIN(StopUpdate)
            FROM DDRigTstLog
            WHERE NameRig = rtl.NameRig
              AND StatBtn = '3'
        ) THEN 'Y'
        ELSE 'N'
    END AS FirstFinish
FROM DDRigTstLog rtl
INNER JOIN DDRigCellDesc rcd 
    ON rtl.CellNo = rcd.CellNo
   AND rtl.NameRig = rcd.NameRig   -- ✅ prevent duplicates
WHERE rtl.NameRig = '" + Line + @"'
ORDER BY rtl.CellNo;
";


            using (var query = new SqlDataAdapter(queryStr, database.Connection))
            {
                query.Fill(tmpTable);

                foreach (DataRow row in tmpTable.Rows)
                {
                    result.Add(new RigModel
                    {
                        SerialNo1 = row["SerialNo1"].ToString(),
                        SerialNo2 = row["SerialNo2"].ToString(),
                        NameRig = row["NameRig"].ToString(),
                        CellNo = row["CellNo"].ToString(),
                        CellName = row["CellName"].ToString(),
                        StatBtn = row["StatBtn"].ToString(),
                        StopUpdate = row["StopUpdate"].ToTypeDateTIme(),
                        DateCheck = row["DateCheck"].ToString(),
                        FirstFinish = row["FirstFinish"].ToString(),


                    });

                }
            }

            return result;
        }
        public List<RigBarModel> GetBarcStat(String Location)
        {


            DataTable tmpTable = new DataTable();
            List<RigBarModel> result = new List<RigBarModel>();

            string queryStr;

            //            if (Location == "DDrig1")
            //            {
            //                queryStr = @"
            //SELECT 
            //    ddrb.CASerial1,
            //    ddrb.CASerial2,
            //    ddrb.Status,
            //    ddrb.Text1,
            //    ddrb.Text2,
            //    ISNULL(ddrd.CellName, '') AS CellName,
            //    ddrb.NameRig
            //FROM DDRigBarcMange ddrb
            //LEFT JOIN DDRigCellDesc ddrd 
            //    ON ddrb.NameRig = ddrd.NameRig
            //   AND SUBSTRING(ddrb.Text1, 2, LEN(ddrb.Text1)) = ddrd.CellNo 
            //           WHERE ddrb.Location = '" + Location + @"'";

            //        }
            //            else
            //            {
            //                queryStr = @"
            //SELECT 
            //    ddrb.CASerial1,
            //    ddrb.CASerial2,
            //    ddrb.Status,
            //    ddrb.Text1,
            //    ddrb.Text2,
            //    ISNULL(ddrd.CellName, '') AS CellName,
            //    ddrb.NameRig
            //FROM DDRigBarcMange ddrb
            //LEFT JOIN DDRigCellDesc ddrd 
            //    ON ddrb.NameRig = ddrd.NameRig
            //   AND SUBSTRING(ddrb.Text1, 2, LEN(ddrb.Text1)) = ddrd.CellNo 
            //                WHERE ddrb.Location = '" + Location + @"'";
            //            }


            queryStr = @"
            SELECT 
                ddrb.CASerial1,
                ddrb.CASerial2,
                ddrb.Status,
                ddrb.Text1,
                ddrb.Text2,
                ISNULL(ddrd.CellName, '') AS CellName,
                ddrb.NameRig
            FROM DDRigBarcMange ddrb
            LEFT JOIN DDRigCellDesc ddrd 
                ON SUBSTRING(ddrb.Text1, 2, LEN(ddrb.Text1)) = ddrd.CellNo ";
                            
        

            using (var query = new SqlDataAdapter(queryStr, database.Connection))
            {
                query.Fill(tmpTable);

                foreach (DataRow row in tmpTable.Rows)
                {
                    result.Add(new RigBarModel
                    {
                        CASerial1 = row["CASerial1"].ToString(),
                        CASerial2 = row["CASerial2"].ToString(),
                        Status = row["Status"].ToString(),
                        Text1 = row["CellName"].ToString(),
                        Text2 = row["Text2"].ToString(),


                    });

                }
            }

            return result;
        }

        public int CheckTesting(String CellName)
        {


            DataTable tmpTable = new DataTable();
            int result = 0;

            string queryStr;


            queryStr = "select ddrg.StatBtn From DDRigTstLog ddrg " +
                "inner join DDRigCellDesc ddrgd on ddrg.CellNo = ddrgd.CellNo " +
                "where ddrgd.CellName  = '" + CellName+"' ";
            using (var query = new SqlDataAdapter(queryStr, database.Connection))
            {
                query.Fill(tmpTable);

                foreach (DataRow row in tmpTable.Rows)
                {
                    result = row["StatBtn"].ToTypeInteger();

                }
            }

            return result;
        }

        public void UpdateBarTable(String CellName)
        {


            DataTable tmpTable = new DataTable();


            string queryStr;
            queryStr = "update DDRigBarcMange set Status ='N', Text1 ='' where Text1  = '" + CellName + "' ";
            using (var query = new SqlDataAdapter(queryStr, database.Connection))
            {
                query.Fill(tmpTable);

                
            }

          
        }

        public void UpdateChecked(String CellName, String Line)
        {


            DataTable tmpTable = new DataTable();


            string queryStr;
            queryStr = "update DDRigTstLog set StatBtn='2' where CellNo= '" + CellName + "' and NameRig='"+ Line + "' ";
            using (var query = new SqlDataAdapter(queryStr, database.Connection))
            {
                query.Fill(tmpTable);


            }


        }
    }
}