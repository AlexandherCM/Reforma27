using Condominios.Data;
using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models.Entities;

namespace Condominios.Models.Services.Classes.SegundoPlano
{
    public class WorkerAutomaticMtos : BackgroundService
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WorkerAutomaticMtos> _logger;

        public WorkerAutomaticMtos(ILogger<WorkerAutomaticMtos> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            //_unitOfWork = unitOfWork;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //List<MtoProgramado> mtos = await _unitOfWork.MtoProgramadoRepository.GetMtos();

                //for(int i = 0;i < mtos.Count; i++)
                //{
                //    Console.WriteLine($"Time Epoch: {mtos[i].UltimaAplicacion}");
                //}
                _logger.LogInformation("Hola mundo", DateTimeOffset.Now);
            }
        }
    }
}
