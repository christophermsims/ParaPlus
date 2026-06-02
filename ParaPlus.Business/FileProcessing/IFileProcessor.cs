namespace ParaPlus.Business.FileProcessing
{
    public interface IFileProcessor<T>
    {
        IEnumerable<T> ProcessFile(string fileToProcess);
    }
}