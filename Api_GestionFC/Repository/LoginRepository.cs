using Api_GestionFC.DTO;
using Api_GestionFC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{
    public class LoginRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public LoginDTO LoginUser(LoginData loginData)
        {
            try
            {
                //Cídigo para hacer el lógin del usuario




                var usuario = new Usuario()
                {
                    Nomina = 23401,
                    NombreCompleto = "Dieguito Maradona",
                    Email = "dnieto@invercap.com.mx"
                };


                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("userData", JsonConvert.SerializeObject(usuario) )
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string Token = tokenHandler.WriteToken(token);

                return new LoginDTO()
                {
                    Usuario = usuario,
                    ResultadoEjecucion = new ResultadoEjecucion() { EjecucionCorrecta = true },
                    Token = Token
                };
            }
            catch (Exception ex)
            {
                return new LoginDTO()
                {
                    Usuario = new Usuario(),
                    ResultadoEjecucion = new ResultadoEjecucion() { EjecucionCorrecta = false, ErrorMessage = ex.Message, FriendlyMessage = "Ocurrió un error" },
                    Token = null
                };
            }
        }


    }
}
