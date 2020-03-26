using Api_GestionFC.DTO;
using Api_GestionFC.Models;
using Api_GestionFC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly LogRepository _repository;

        public LogController(LogRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<LogSistemaDTO> SetLogSistema([FromBody] LogSistema logSistema)
        {
            try
            {
                return await _repository.LogSistema(logSistema);
            }
            catch (Exception ex)
            {
                return new LogSistemaDTO()
                {
                    LogSistema = new LogSistema(),
                    ResultadoEjecucion = new ResultadoEjecucion() { EjecucionCorrecta = false, ErrorMessage = ex.Message, FriendlyMessage = "Ocurrió un error" }
                };
            }
        }
    }
}
