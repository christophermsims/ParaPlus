using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParaPlus.Business.Helper
{
    public static class LinkConstants
    {
        private static string _quarterlyPatentFilings = "https://ipfolio-4320.lightning.force.com/lightning/r/Report/00OPd00000A2IxZMAV/view?queryScope=userFolders";
        private static string _quarterlyPatentIssuances = "https://ipfolio-4320.lightning.force.com/lightning/r/Report/00O5f00000886mBEAQ/view?queryScope=userFolders";
        private static string _quarterlyOnePagers = "https://ipfolio-4320.lightning.force.com/lightning/r/Report/00OPd000008zxo9MAA/view?queryScope=userFolders";
        private static string _quarterlyIssuedInventorAwardsDue = "https://ipfolio-4320.lightning.force.com/lightning/r/Report/00OPd000009KhA6MAK/view?queryScope=userFolders";
        private static string _masterAwardsFile = "https://docs.google.com/spreadsheets/d/1MlukHMFDhuON04K8fRh4GTR1zmxeXry_/edit?usp=drive_link&ouid=115140695021392269146&rtpof=true&sd=true";

		public static string QuaterlyPatentFilings { get { return _quarterlyPatentFilings; } }
        public static string QuaterlyPatentIssuances { get { return _quarterlyPatentIssuances; } }
        public static string QuaterlyOnePagers { get { return _quarterlyOnePagers; } }
        public static string QuarterlyIssuedInvetorAwardsDue {  get { return _quarterlyIssuedInventorAwardsDue; } }
		public static string MasterAwardsFile { get { return _masterAwardsFile; } }
	}
}
