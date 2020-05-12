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
    [Authorize]
    public class ProductividadController
    {
        private readonly Repository.ProductividadRepository _repository;

        public ProductividadController(Repository.ProductividadRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetProductividadDiaria/{nomina}/{Anio}/{SemanaAnio}")]
        public async Task<DTO.ProductividadDiariaDTO> GetProductividadDiaria(int nomina, int Anio = 0, int SemanaAnio = 0)
        {
            var response = new DTO.ProductividadDiariaDTO();
            try
            {
                response = await _repository.GetProductividadDiaria(nomina, Anio, SemanaAnio);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetComisionEstimada/{nomina}/{Fecha}")]
        public async Task<DTO.ComisionEstimadaDTO> GetComisionEstimada(int nomina, DateTime Fecha = new DateTime())
        {
            var response = new DTO.ComisionEstimadaDTO();
            try
            {
                response = await _repository.GetComisionEstimada(nomina, Fecha);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }
        
        [HttpGet("GetProductividadSemanal/{nomina}/{Anio}/{TetrasemanaAnio}")]
        public async Task<DTO.ProductividadSemanalDTO> GetProductividadSemanal(int nomina, int Anio = 0, int TetrasemanaAnio = 0)
        {
            var response = new DTO.ProductividadSemanalDTO();
            try
            {
                response = await _repository.GetProductividadSemanal(nomina, Anio, TetrasemanaAnio);
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
