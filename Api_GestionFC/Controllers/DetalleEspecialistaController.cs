using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleEspecialistaController
    {
        private readonly Repository.DetalleEspecialistaRepository _repository;

        public DetalleEspecialistaController(Repository.DetalleEspecialistaRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetDetalleEspecialista/{nominaAP}/{nomina}")]
        public async Task<DTO.DetalleEspecialistaDTO> GetAlertasPlantillaImproductividad(int nominaAP, int nomina)
        {
            var response = new DTO.DetalleEspecialistaDTO();
            try
            {
                response = await _repository.GetDetalleEspecialista(nominaAP, nomina);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetDetalleFolio/{folioSolicitud}")]
        public async Task<DTO.DetalleFolioDTO> GetDetalleFolio(string folioSolicitud)
        {
            var response = new DTO.DetalleFolioDTO();
            try
            {
                response = await _repository.GetDetalleFolio(folioSolicitud);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetDetalleEspecialistaHistorico/{nomina}/{Fecha}")]
        public async Task<DTO.DetalleHistoricoDTO> GetDetalleEspecialistaHistorico(int nomina, DateTime Fecha)
        {
            var response = new DTO.DetalleHistoricoDTO();
            try
            {
                response = await _repository.GetDetalleEspecialistaHistorico(nomina,Fecha);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }
    }
}
