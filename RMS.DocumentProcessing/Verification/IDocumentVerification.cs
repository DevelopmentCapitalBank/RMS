using System.Data;

namespace RMS.DocumentProcessing.Verification
{
    public enum TypeDocument
    {
        VisList = 1,
        Turnovers = 2,
        Deposits = 3,
        Operation = 4,
        Conversion = 5

    }
    public interface IDocumentVerification
    {
        bool IsVerified(TypeDocument document, DataTable dataTable);
    }
}
