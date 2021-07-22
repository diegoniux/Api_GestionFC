using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaController
    {
        private readonly Repository.AlertaRepository _repository;

        public AlertaController(Repository.AlertaRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetAlertasPlantillaImproductividad/{nomina}/{nominaAP?}")]
        public async Task<DTO.AlertaImproductividadDTO> GetAlertasPlantillaImproductividad(int nomina, int nominaAP = 0)
        {
            var response = new DTO.AlertaImproductividadDTO();
            try
            {
                response = await _repository.GetAlertaImproductividad(nomina, nominaAP);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetAlertasPlantillaRecuperacion/{nomina}")]
        public async Task<DTO.AlertaRecuperacionDTO> GetAlertasPlantillaRecuperacion(int nomina)
        {
            var response = new DTO.AlertaRecuperacionDTO();
            try
            {
                response = await _repository.GetAlertaRecuperacion(nomina);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetAlertasPlantillaInvestigacion/{nomina}")]
        public async Task<DTO.AlertaInvestigacionDTO> GetAlertasPlantillaInvestigacion(int nomina)
        {
            var response = new DTO.AlertaInvestigacionDTO();
            try
            {
                response = await _repository.GetAlertaInvestigacion(nomina);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetAlertasPlantillaSinSaldoVirtual/{nomina}")]
        public async Task<DTO.AlertaSinSaldoVirtualDTO> GetAlertasPlantillaSinSaldoVirtual(int nomina)
        {
            var response = new DTO.AlertaSinSaldoVirtualDTO();
            try
            {
                response = await _repository.GetAlertaSinSaldoVirtual(nomina);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetAlertasPlantillaSeguimientoSinSaldoVirtual/{nomina}/{idalerta}")]
        public async Task<DTO.AlertaSinSaldoVirtualDTO> GetAlertasPlantillaSeguimientoSinSaldoVirtual(int nomina, int idAlerta)
        {
            var response = new DTO.AlertaSinSaldoVirtualDTO();
            try
            {
                response = await _repository.GetAlertaSeguimientoSinSaldoVirtual(nomina, idAlerta);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }
        
        [HttpGet("GetFoliosRecuperacion/{nomina}")]
        public async Task<DTO.FoliosRecuperacionDTO> GetFoliosRecuperacion(int nomina)
        {
            var response = new DTO.FoliosRecuperacionDTO();
            try
            {
                response = await _repository.GetFoliosRecuperacion(nomina);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetDetalleFolioRecuperacion/{RegistroTraspasoId}/{PantallaId}")]
        public async Task<DTO.AlertaRecuperacionDTO> GetDetalleFolioRecuperacion(int RegistroTraspasoId, int PantallaId)
        {
            var response = new DTO.AlertaRecuperacionDTO();
            try
            {
                response = await _repository.GetDetalleFolioRecuperacion(RegistroTraspasoId,PantallaId);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetMensajeGerente/{Nomina}")]
        public async Task<DTO.AlertaMensajeDTO> GetMensajeGerente(int Nomina)
        {
            var response = new DTO.AlertaMensajeDTO();
            try
            {
                response = await _repository.GetMensajeGerente(Nomina);
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
