namespace ParaPlus.Business.Model
{
    public readonly record struct FiscalQuarter(int Year, int Quarter) : IComparable<FiscalQuarter>
    {
        public int CompareTo(FiscalQuarter other) => Year != other.Year ? Year.CompareTo(other.Year) : Quarter.CompareTo(other.Quarter);

        public override string ToString() => $"{Year} Q{Quarter}";

        public static bool TryParse(string? quarterString, out FiscalQuarter quarter)
        {
            quarter = default;

            if (string.IsNullOrWhiteSpace(quarterString))
            {
                return false;
            }

            var parts = quarterString.Trim().Split(' ');
            if (parts.Length != 2)
            {
                return false;
            }

            if (!int.TryParse(parts[0], out var year))
            {
                return false;
            }

            if (!parts[1].StartsWith("Q") || !int.TryParse(parts[1].Substring(1), out var quarterNumber))
            {
                return false;
            }

            if (quarterNumber < 1 || quarterNumber > 4)
            {
                return false;
            }

            quarter = new FiscalQuarter(year, quarterNumber);
            return true;
        }
    }
}
