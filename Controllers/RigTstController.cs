using DDRigWeb.Business_logic;
using DDRigWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDRigWeb.Controllers
{

    [RoutePrefix("RigDashB")]
    public class RigTstController : Controller
    {
        private DDRigBS ddRigBS; //Business Logic

        public RigTstController()
        {
            ddRigBS = new DDRigBS();

        }


        [Route("RigTst")]
        public ActionResult RigTst(string lineid)
        {
            if (string.IsNullOrEmpty(lineid))
            {
                lineid = "Line101";
            }
            ViewBag.lineid = lineid;
            List<List<RigModel>> listModels = new List<List<RigModel>>();
            List<RigModel> AllRigs = new List<RigModel>();

            List<RigModel> RigA = new List<RigModel>();
            List<RigModel> RigB = new List<RigModel>();

            List<RigModel> RigC = new List<RigModel>();
            List<RigModel> RigD = new List<RigModel>();
            List<RigModel> RigE = new List<RigModel>();
            AllRigs = ddRigBS.GetAllRigs(lineid);

            for (int i = 0; i < AllRigs.Count ; i++)
            {
               if(AllRigs[i].DateCheck == "OK" && AllRigs[i].StatBtn == "1")
                {
                    ddRigBS.UpdateChecked(AllRigs[i].CellNo, lineid);
                    AllRigs[i].StatBtn = "2";
                }
            
            }


            for (int i = 0; i < 9; i++)
            {
                RigA.Add(AllRigs[i]);
            }
            for (int i = 9; i < 18; i++)
            {
                RigB.Add(AllRigs[i]);
            }
            for (int i = 18; i < 27; i++)
            {
                RigC.Add(AllRigs[i]);
            }
            for (int i = 27; i < 36; i++)
            {
                RigD.Add(AllRigs[i]);
            }
            for (int i = 36; i < 45; i++)
            {
                RigE.Add(AllRigs[i]);
            }

            ViewBag.Selected = "RigTst";

            listModels.Add(RigA);
            listModels.Add(RigB);
            listModels.Add(RigC);
            listModels.Add(RigD);
            listModels.Add(RigE);
            if (lineid == "Line101")
                return View("AllRigViewTV1", listModels);
            else
                return View("AllRigViewTV2", listModels);
        }

      
        [Route("RefreshTables")]
        public ActionResult RefreshTables(string lineid)
        {
          //  lineid = "Line101";
            ViewBag.lineid = lineid;
            List<List<RigModel>> listModels = new List<List<RigModel>>();
            List<RigModel> AllRigs = new List<RigModel>();

            List<RigModel> RigA = new List<RigModel>();
            List<RigModel> RigB = new List<RigModel>();

            List<RigModel> RigC = new List<RigModel>();
            List<RigModel> RigD = new List<RigModel>();
            List<RigModel> RigE = new List<RigModel>();
            AllRigs = ddRigBS.GetAllRigs(lineid);


            for (int i = 0; i < AllRigs.Count; i++)
            {
                if (AllRigs[i].DateCheck == "OK" && AllRigs[i].StatBtn=="1")
                {
                    ddRigBS.UpdateChecked(AllRigs[i].CellNo, lineid);
                    AllRigs[i].StatBtn = "2";
                }

            }

            for (int i = 0; i < 9; i++)
            {
                RigA.Add(AllRigs[i]);
            }
            for (int i = 9; i < 18; i++)
            {
                RigB.Add(AllRigs[i]);
            }
            for (int i = 18; i < 27; i++)
            {
                RigC.Add(AllRigs[i]);
            }
            for (int i = 27; i < 36; i++)
            {
                RigD.Add(AllRigs[i]);
            }
            for (int i = 36; i < 45; i++)
            {
                RigE.Add(AllRigs[i]);
            }

            ViewBag.Selected = "RefreshTables";

            listModels.Add(RigA);
            listModels.Add(RigB);
            listModels.Add(RigC);
            listModels.Add(RigD);
            listModels.Add(RigE);
            if (lineid == "Line101")
                return PartialView("_RigTablesTV1", listModels);
            else                 
                return PartialView("_RigTablesTV2", listModels);

        }


        [Route("RefreshBtn")]
        public ActionResult RefreshBtn(string lineid)
        {
            ViewBag.lineid = lineid;
        //    lineid = "Line101";
            List<List<RigModel>> listModels = new List<List<RigModel>>();
            List<RigModel> AllRigs = new List<RigModel>();

            List<RigModel> RigA = new List<RigModel>();
            List<RigModel> RigB = new List<RigModel>();

            List<RigModel> RigC = new List<RigModel>();
            List<RigModel> RigD = new List<RigModel>();
            List<RigModel> RigE = new List<RigModel>();
            AllRigs = ddRigBS.GetAllRigs(lineid);

            for (int i = 0; i < 9; i++)
            {
                RigA.Add(AllRigs[i]);
            }
            for (int i = 9; i < 18; i++)
            {
                RigB.Add(AllRigs[i]);
            }
            for (int i = 18; i < 27; i++)
            {
                RigC.Add(AllRigs[i]);
            }
            for (int i = 27; i < 36; i++)
            {
                RigD.Add(AllRigs[i]);
            }
            for (int i = 36; i < 45; i++)
            {
                RigE.Add(AllRigs[i]);
            }

            

            listModels.Add(RigA);
            listModels.Add(RigB);
            listModels.Add(RigC);
            listModels.Add(RigD);
            listModels.Add(RigE);
            var statuses = listModels.SelectMany(list => list.Select(r => new {
                r.CellNo,
                r.CellName,
                r.StatBtn,
                r.FirstFinish
            })).ToList();

            return Json(statuses, JsonRequestBehavior.AllowGet);

        }

     

        //RIG BAR =======================================================
        [Route("RigBar")]
        public ActionResult RigBar()
        {
            ViewBag.Selected = "RigBar";
          
            // List<List<RigBarModel>> listModels = new List<List<RigBarModel>>();
            List<RigBarModel> RigBarM = new List<RigBarModel>();


            RigBarM = ddRigBS.GetBarcStat("DDrig1");
            RigBarM[0].Text2 = RigBarM[0].Text1;
            RigBarM.AddRange(ddRigBS.GetBarcStat("DDrig2"));
            RigBarM[1].Text2 = RigBarM[1].Text1;
            return View("RigBar", RigBarM);
        }

        [Route("GetLatestData")]
        public JsonResult GetLatestData()
        {
            int StatName =0;
            var rigBarM = new List<RigBarModel>();
            rigBarM = ddRigBS.GetBarcStat("DDrig1");
            StatName = ddRigBS.CheckTesting(rigBarM[0].Text1);
            rigBarM[0].Text2 = rigBarM[0].Text1;
            //if (StatName > 0)
            //{
            //    //CELL NOT READY
            //    ddRigBS.UpdateBarTable(rigBarM[0].Text1);
            //    rigBarM[0].Text1 = "";

            //}
            //;


            rigBarM.AddRange(ddRigBS.GetBarcStat("DDrig2"));       
            StatName = ddRigBS.CheckTesting(rigBarM[1].Text1);
            rigBarM[1].Text2 = rigBarM[1].Text1;
            //if (StatName > 0)
            //{
            //    //CELL NOT READY
            //    ddRigBS.UpdateBarTable(rigBarM[1].Text1);
            //    rigBarM[1].Text1 = "";
            //}
            //;
            if (rigBarM[0].CASerial1 ==  rigBarM[0].CASerial2 )
            {
                rigBarM[0].CASerial2 = "";
            }

            if (rigBarM[1].CASerial1 == rigBarM[1].CASerial2)
            {
                rigBarM[1].CASerial2 = "";
            }
            // Return both monitors as JSON
            var result = rigBarM.Select(x => new {
                CASerial1 = x.CASerial1,
                CASerial2 = x.CASerial2,
                Text1 = x.Text1,
                Text2 = x.Text2,
                Status = x.Status
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
    }
}