using ParaPlus.Business.Model;

namespace ParaPlus.Business.FileProcessing
{
    public interface IQuarterlySummaryService<T>
    {
        QuarterlySummaryReport GetQuarterlySummary();
        QuarterlySummaryReport GetQuarterlySummary(string? startQuarter, string? endQuarter);

	}
}
