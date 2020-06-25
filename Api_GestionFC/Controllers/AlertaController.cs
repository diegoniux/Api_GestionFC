﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetAlertasPlantillaImproductividad/{nomina}")]
        public async Task<DTO.AlertaImproductividadDTO> GetAlertasPlantillaImproductividad(int nomina)
        {
            var response = new DTO.AlertaImproductividadDTO();
            try
            {
                response = await _repository.GetAlertaImproductividad(nomina);
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