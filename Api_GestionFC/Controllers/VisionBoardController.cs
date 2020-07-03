using Api_GestionFC.DTO;
using Api_GestionFC.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisionBoardController
    {

        private readonly VisionBoardRepository _repository;

        public VisionBoardController(VisionBoardRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetMetaPlantilla/{nomina}")]
        public async Task<MetaPlantillaDTO> GetMetaPlantilla(int nomina)
        {
            var response = new DTO.MetaPlantillaDTO();
            try
            {
                response = await _repository.GetMetaPlantilla(nomina);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetMetaPlantillaIndividual/{nomina}")]
        public async Task<MetaPlantillaIndividualDTO> GetMetaPlantillaIndividual(int nomina)
        {
            var response = new MetaPlantillaIndividualDTO();
            try
            {
                response = await _repository.GetMetaPlantillaIndividual(nomina);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpPost("RegistrarMetaPlantilla")]
        public async Task<MetaPlantillaResponseDTO> RegistrarMetaPlantilla(MetaPlantillaRequestDTO MetaPlantilla)
        {
            var response = new MetaPlantillaResponseDTO();
            try
            {
                response = await _repository.RegistrarMetaPlantilla(MetaPlantilla);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpPost("RegistrarMetaPlantillaIndividual")]
        public async Task<MetaPlantillaIndividualResponseDTO> RegistrarMetaPlantillaIndividual(MetaPlantillaIndividualRequestDTO MetaPlantillaIndividual)
        {
            var response = new MetaPlantillaIndividualResponseDTO();
            try
            {
                response = await _repository.RegistrarMetaPlantillaIndividual(MetaPlantillaIndividual);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpPost("RegistrarMetaPlantillaFolios")]
        public async Task<MetaPlantillaFoliosResponseDTO> RegistrarMetaPlantillaFolios(MetaPlantillaFoliosRequestDTO MetaPlantilla)
        {
            var response = new MetaPlantillaFoliosResponseDTO();
            try
            {
                response = await _repository.RegistrarMetaPlantillaFolios(MetaPlantilla);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpPost("RegistrarMetaPlantillaSaldoAcumulado")]
        public async Task<MetaPlantillaSaldoAcumuladoResponseDTO> RegistrarMetaPlantillaSaldoAcumulado(MetaPlantillaSaldoAcumuladoRequestDTO MetaPlantillaSaldoAcumulado)
        {
            var response = new MetaPlantillaSaldoAcumuladoResponseDTO();
            try
            {
                response = await _repository.RegistrarMetaPlantillaSaldoAcumulado(MetaPlantillaSaldoAcumulado);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpPost("RegistrarMetaPlantillaComisionSem")]
        public async Task<MetaPlantillaComisionSemResponseDTO> RegistrarMetaPlantillaComisionSem(MetaPlantillaComisionSemRequestDTO MetaPlantillaComisionSem)
        {
            var response = new MetaPlantillaComisionSemResponseDTO();
            try
            {
                response = await _repository.RegistrarMetaPlantillaComisionSem(MetaPlantillaComisionSem);
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
