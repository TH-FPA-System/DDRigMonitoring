using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDRigWeb.Models
{
    public class RigModel
    {
        public RigModel()
        {
        }
 
        public string CellNo { get; set; } = "0";
        public string CellName { get; set; } = "0";

        public string SerialNo1 { get; set; } = "";
        public string SerialNo2 { get; set; } = "";
        public string NameRig { get; set; } = "";
        public string StatBtn { get; set; } = "Empty";
        public DateTime StartUpdate { get; set; } = DateTime.Now;
        public DateTime StopUpdate { get; set; } = DateTime.Now;

        public string DateCheck { get; set; } = "NG";
        public string FirstFinish { get; set; } = "N";

    }

    public class RigBarModel
    {
        public RigBarModel()
        {
        }
        public int ID_RIGBARC { get; set; } = 0;
        public string Location { get; set; } = "";
        public string CASerial1 { get; set; } = "";
        public string CASerial2 { get; set; } = "";
        public string Location2 { get; set; }
        public string Status { get; set; } = "";
        public string Text1 { get; set; } = "";
        public string Text2 { get; set; } = "";

    }
}