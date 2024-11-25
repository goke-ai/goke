
namespace GokeWebApp.Client.Services
{
    public interface IDataProcessingService
    {
        Task<string> ProcessData(string endpoint, FormModel model);
    }

    public class FormModel
    {
        public string Message { get; set; } = string.Empty;
    }
}