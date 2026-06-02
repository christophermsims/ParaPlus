using System;

namespace ParaPlus.Business.Helper
{
    public static class FiscalYear
    {
        // Fiscal year starts on February 1. The fiscal year label is the calendar year
        // in which the fiscal year ends (e.g. fiscal year that starts Feb 1, 2026 is FY 2027).

        public static string CurrentFiscalYear => GetFiscalYear(DateTime.Today).ToString();

        public static string CurrentFiscalQuarter => "Q" + GetFiscalQuarter(DateTime.Today).ToString();

        public static string LastFiscalYear => GetLastFiscal(DateTime.Today).year.ToString();

        public static string LastFiscalQuarter => "Q" + GetLastFiscal(DateTime.Today).quarter.ToString();

        private static int GetFiscalYear(DateTime date)
        {
            return date.Month >= 2 ? date.Year + 1 : date.Year;
        }

        private static int GetFiscalQuarter(DateTime date)
        {
            int m = date.Month;
            if (m >= 2 && m <= 4) return 1;   // Feb-Apr => Q1
            if (m >= 5 && m <= 7) return 2;   // May-Jul => Q2
            if (m >= 8 && m <= 10) return 3;  // Aug-Oct => Q3
            return 4;                         // Nov-Jan => Q4
        }

        private static (int year, int quarter) GetLastFiscal(DateTime date)
        {
            int currentFiscalYear = GetFiscalYear(date);
            int currentFiscalQuarter = GetFiscalQuarter(date);

            int lastQuarter = currentFiscalQuarter - 1;
            int lastYear = currentFiscalYear;
            if (lastQuarter < 1)
            {
                lastQuarter = 4;
                lastYear -= 1;
            }

            return (lastYear, lastQuarter);
        }
    }
}
