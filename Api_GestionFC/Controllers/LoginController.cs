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
    public class LoginController: ControllerBase
    {
        private readonly LoginRepository _repository;

        public LoginController(LoginRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public LoginDTO LoginUser(LoginData loginData)
        {
            try
            {
                return _repository.LoginUser(loginData);
            }
            catch (Exception ex)
            {
                return new LoginDTO()
                {
                    ResultadoEjecucion = new ResultadoEjecucion() { EjecucionCorrecta = false, ErrorMessage = ex.Message, FriendlyMessage = "Ocurrió un error" }
                };
            }
        }
    }
}
