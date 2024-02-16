using Application.Interfaces;
using Domain.Entities;

namespace AutorizacionesAprobadas
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageService _messageService;
        private readonly IContextMgmt _contextMgmt;

        public Worker(ILogger<Worker> logger, IMessageService messageSenderService, IContextMgmt contextMgmt)
        {
            _messageService = messageSenderService;
            _logger = logger;
            _contextMgmt = contextMgmt;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //Leer el msj
                    var solicitud = await _messageService.ConsumeAsync();
                    _logger.LogInformation("Solicitud Recibida: {IdSolicitud}", solicitud.IdSolicitud);

                    //Inserta registro en la base
                    _logger.LogInformation("Inicio insertar solicitud aprobada: {IdSolicitud}", solicitud.IdSolicitud);
                    SolicitudAprobada solAprob = new();
                    solAprob.IdSolicitud = solicitud.IdSolicitud;
                    solAprob.Cliente = solicitud.Cliente;
                    solAprob.Monto = solicitud.Monto;
                    solAprob.Fecha = DateTime.Now;
                    _contextMgmt.InsertSolicitud(solAprob);
                    _logger.LogInformation("Fin insertar solicitud aprobada: {IdSolicitud}", solicitud.IdSolicitud);


                }
                catch (Exception ex)
                {
                    _logger.LogError("Error: {message}", ex.Message);
                }
            }
        }
    }
}