using Condominios.Data.Interfaces;

namespace Condominios.Models.Services
{
    public class FileDataService : IFileData
    {
        public async Task Create(string path, int n)
        {
            await File.WriteAllTextAsync(path, $"Document {n}");
            await Task.Delay(5000);
        }
    }
}
