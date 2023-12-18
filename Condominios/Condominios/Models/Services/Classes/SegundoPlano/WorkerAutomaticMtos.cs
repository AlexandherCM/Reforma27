using Condominios.Data;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Data.Repositories.Catalogos;
using Condominios.Data.Repositories.Mantenimientos;
using Condominios.Models.Entities;
#pragma warning disable CS8600
#pragma warning disable CS8602

namespace Condominios.Models.Services.Classes.SegundoPlano
{
    public class WorkerAutomaticMtos : BackgroundService
    {
        private readonly ILogger<WorkerAutomaticMtos> _logger;
        private readonly IServiceProvider _serviceProvider;

        public WorkerAutomaticMtos(ILogger<WorkerAutomaticMtos> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var timeUntilNextExecution = AdjustDeadlines(23, 59, 0);

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IMtoRepository mtoRepository = scope.ServiceProvider.GetService<IMtoRepository>();

                    await mtoRepository.CreateNewMtoProgram();
                }

                // Tiempo restante
                Console.WriteLine($"Tiempo restante para ejecutar nuevamente la tarea: " +
                    $"{timeUntilNextExecution.TotalHours} horas, {timeUntilNextExecution.TotalMinutes} minutos, {timeUntilNextExecution.TotalSeconds} segundos");

                // Espera hasta la próxima ejecución
                //await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Delay de 5 segundos
                await Task.Delay(timeUntilNextExecution, stoppingToken);
            }
        }

        private TimeSpan AdjustDeadlines(int Hour, int minutes, int seconds) 
        {
            var NowTime = DateTime.Now;
            var nextExecutionTimeToday = new DateTime(NowTime.Year, NowTime.Month, NowTime.Day, Hour, minutes, seconds);

            // Si la hora actual es después de la hora programada para hoy, se ajusta la próxima ejecución para mañana
            if (NowTime >= nextExecutionTimeToday)
            {
                nextExecutionTimeToday = nextExecutionTimeToday.AddDays(1);
            }

            // Se calcula el tiempo restante hasta la próxima ejecución
            return nextExecutionTimeToday - NowTime;
        }

    }
}
