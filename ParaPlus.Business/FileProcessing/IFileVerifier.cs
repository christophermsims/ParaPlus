namespace ParaPlus.Business.FileProcessing
{
    public interface IFileVerifier
    {
        bool VerifyHeaders(string[] headers);
    }
}