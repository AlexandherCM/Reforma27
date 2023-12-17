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
                var timeUntilNextExecution = AdjustDeadlines(0, 7, 0);

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IMtoRepository mtoRepository = scope.ServiceProvider.GetService<IMtoRepository>();
                    List<MtoProgramado> Mantenimientos = await mtoRepository.GetMtosProgramados();

                    if (Mantenimientos.Any())
                        await UpdatePeriods(mtoRepository, Mantenimientos);
                }
                
                // tiempo restante
                Console.WriteLine($"Tiempo restante para ejecutar nuevamente la tarea: " +
                    $"{timeUntilNextExecution.TotalHours} horas, {timeUntilNextExecution.TotalMinutes} minutos, {timeUntilNextExecution.TotalSeconds} segundos");


                // Espera hasta la próxima ejecución
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

        private async Task UpdatePeriods(IMtoRepository mtoRepository, List<MtoProgramado> Mtos)
        {
            var NowTime = DateTime.Now;

            Mtos.ForEach(mto =>
            {
                var ProximaAplicacion = mtoRepository.ObtenerFecha(mto.ProximaAplicacion);
                int comparacion = NowTime.CompareTo(ProximaAplicacion);

                if (comparacion > -1)
                {
                    MtoProgramado newMto = new()
                    {
                        EquipoID = mto.Equipo.ID,
                        UltimaAplicacion = mto.ProximaAplicacion,
                        ProximaAplicacion = mtoRepository.CrearEpoch(ProximaAplicacion.AddSeconds(mto.Equipo.Variante.Periodo.Meses)),
                        Aplicable = true,
                        Aplicado = false
                    };

                    // Agregar a la base de datos
                    mtoRepository.AddEntity(newMto);
                    mto.Aplicable = false;
                }
            });

            // Guardar todos los cambios de la base de datos
            await mtoRepository.Save();
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        using (IServiceScope scope = _serviceProvider.CreateScope())
        //        {
        //            IMtoRepository mtoRepository = scope.ServiceProvider.GetService<IMtoRepository>();
        //            List<MtoProgramado> Mtos = await mtoRepository.GetMtosProgramados();

        //            DateTime NowTime = DateTime.Now;

        //            if (Mtos.Any())
        //            {
        //                Mtos.ForEach(mto =>
        //                {
        //                    var ProximaAplicacion = mtoRepository.ObtenerFecha(mto.ProximaAplicacion);
        //                    int comparacion = NowTime.CompareTo(ProximaAplicacion);

        //                    if (comparacion > -1)
        //                    {
        //                        MtoProgramado newMto = new()
        //                        {
        //                            EquipoID = mto.Equipo.ID,
        //                            UltimaAplicacion = mto.ProximaAplicacion,
        //                            ProximaAplicacion = mtoRepository.CrearEpoch(ProximaAplicacion.AddSeconds(mto.Equipo.Variante.Periodo.Meses)),
        //                            Aplicable = true,
        //                            Aplicado = false
        //                        };

        //                        // Agregar a la base de datos
        //                        mtoRepository.AddEntity(newMto);
        //                        mto.Aplicable = false;
        //                    }
        //                });

        //                await mtoRepository.Save();
        //                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Delay de 5 segundos
        //            }
        //            else
        //                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Delay de 5 segundos
        //        }
        //    }
        //}

    }
}
