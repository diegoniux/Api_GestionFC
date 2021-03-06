﻿using Api_GestionFC.DTO;
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
    public class LoginController: ControllerBase
    {
        private readonly LoginRepository _repository;

        public LoginController(LoginRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        [AllowAnonymous]
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
                    Usuario = new Usuario(),
                    ResultadoEjecucion = new ResultadoEjecucion() { EjecucionCorrecta = false, ErrorMessage = ex.Message, FriendlyMessage = "Ocurrió un error" }
                };
            }
        }

        [HttpGet]
        [Authorize]
        public Usuario GetUser()
        {
            try
            {
                return new Usuario() { Nomina = 23401, NombreCompleto = "Dieguito Maradona", Email = "dnieto@invercap.com.mx" };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
