using System;
using System.Collections.Generic;
using System.Text;

namespace ParaPlus.Business.Helper
{
	public static class OutputFiles
	{
		private static string _quarterlyIssuedAwardsOutputFile = @"Quarterly Output.csv";
		private static string _quarterlyChineseInventorIssuedAwardsOutputFile = @"Quarterly Chinese Inventor Output.csv";
		private static string _masterInventorWardsOutputFile = "Master Output.csv";
		private static string _vendorDomesticOutputFile = "Vendor Domestic Output.csv";
		private static string _vendorInternationalOutputFile = "Vendor International Output.csv";
		private static string _vendorChineseOutputFile = "Vendor Chinese Output.csv";

		public static string QuarterlyIssuedAwardsOutputFile { get { return _quarterlyIssuedAwardsOutputFile; } }
		public static string QuarterlyChineseAwardIssuedAwardsOutputFile { get { return _quarterlyChineseInventorIssuedAwardsOutputFile; } }
		public static string MasterInventorWardsOutputFile { get { return _masterInventorWardsOutputFile; } }
		public static string VendorDomesticOutputFile { get { return _vendorDomesticOutputFile; } }
		public static string VendorInternationalOutputFile { get { return _vendorInternationalOutputFile; } }
		public static string VendorChineseOutputFile { get { return _vendorChineseOutputFile; } }
	}
}
