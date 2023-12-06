using Condominios.Data.Interfaces;

namespace Condominios.Models.Services.Classes.SegundoPlano
{

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFileData _fileData;
        private int _count = 0;
        public Worker(ILogger<Worker> logger, IFileData FileData)
        {
            _logger = logger;
            _fileData = FileData;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string fileName = Guid.NewGuid().ToString();
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", fileName);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _fileData.Create($"{filePath}.txt", _count++);
            }
        }
    }
}
