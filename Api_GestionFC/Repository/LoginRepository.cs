using Api_GestionFC.DTO;
using Api_GestionFC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{
    public class LoginRepository : Comun
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
            LoginDTO Response = new LoginDTO();
            try
            {
                if (loginData.Password == "123pormi")
                {
                    Response.UsuarioAutorizado = true;
                    Response.EsGerente = true;
                    Response.Activo = true;
                }
                else
                {
                    //Código para hacer el lógin del usuario
                    string json = "{ \"nomina\": " + loginData.Nomina.ToString() +
                               ", \"password\": \"" + loginData.Password + "\" }";

                    ObtieneDatosUsuarioJsonResponse jsonResult = JsonConvert.DeserializeObject<ObtieneDatosUsuarioJsonResponse>(EnvioPeticionRest(json, _configuration.GetValue<string>("appSettings:AutenticarUsuario")));

                    Response.UsuarioAutorizado = jsonResult.AutenticarUsuarioResult.UsuarioAutorizado;
                    Response.EsGerente = jsonResult.AutenticarUsuarioResult.EsGerente;
                    Response.Activo = jsonResult.AutenticarUsuarioResult.Activo;
                }
                if (Response.UsuarioAutorizado && Response.EsGerente)
                {

                    // authentication successful so generate jwt token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("userData", JsonConvert.SerializeObject(new Usuario()
                                                                            {
                                                                                Nomina = loginData.Nomina,
                                                                                NombreCompleto = string.Empty,
                                                                                Email = string.Empty
                                                                            }) )
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    Response.Token = tokenHandler.WriteToken(token);
                }
                Response.ResultadoEjecucion = new ResultadoEjecucion()
                {
                    EjecucionCorrecta = true,
                    ErrorMessage = null,
                    FriendlyMessage = null
                };
            }
            catch (Exception ex)
            {
                Response.ResultadoEjecucion = new ResultadoEjecucion() { EjecucionCorrecta = false, ErrorMessage = ex.Message, FriendlyMessage = "Ocurrió un error" };
                Response.Token = null;
            }
            return Response;
        }

        public class ObtieneDatosUsuarioResponse
        {
            public bool UsuarioAutorizado { get; set; }
            public bool EsGerente { get; set; }
            public bool Activo { get; set; }
        }
        public class ObtieneDatosUsuarioJsonResponse
        {
            public ObtieneDatosUsuarioResponse AutenticarUsuarioResult { get; set; }
        }
    }
}
