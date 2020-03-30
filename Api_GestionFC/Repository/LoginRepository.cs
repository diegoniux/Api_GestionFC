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
    public class LoginRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public string EnvioPeticionRest(string json)
        {
            string Resultado = string.Empty;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://desaiis01/ICAP_AfiliacionServices_GFC/GerentesFC/GerentesFCService.svc/ObtieneDatosUsuario");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = 600000;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Resultado = result.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }

        public LoginDTO LoginUser(LoginData loginData)
        {
            LoginDTO Response = new LoginDTO();
            try
            {
                //Código para hacer el lógin del usuario
                string json = "{ \"nomina\": " + loginData.Nomina.ToString() + 
                               ", \"password\": \"" + loginData.Password + "\" }";

                ObtieneDatosUsuarioJsonResponse jsonResult = JsonConvert.DeserializeObject<ObtieneDatosUsuarioJsonResponse>(EnvioPeticionRest(json));

                Response.UsuarioAutorizado = jsonResult.ObtieneDatosUsuarioResult.Autorizado;
                Response.Usuario = new Usuario()
                {
                    Nomina = loginData.Nomina,
                    NombreCompleto = jsonResult.ObtieneDatosUsuarioResult.NombreCompleto,
                    Email = string.Empty
                };

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("userData", JsonConvert.SerializeObject(Response.Usuario) )
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                Response.Token = tokenHandler.WriteToken(token);
                Response.ResultadoEjecucion = new ResultadoEjecucion() 
                { 
                    EjecucionCorrecta = true, 
                    ErrorMessage = null, 
                    FriendlyMessage = null 
                };
            }
            catch (Exception ex)
            {
                Response.Usuario = null;
                Response.ResultadoEjecucion = new ResultadoEjecucion() { EjecucionCorrecta = false, ErrorMessage = ex.Message, FriendlyMessage = "Ocurrió un error" };
                Response.Token = null;
            }
            return Response;
        }

        public class ObtieneDatosUsuarioResponse
        {
            public string NombreCompleto { get; set; }
            public bool Autorizado { get; set; }
        }
        public class ObtieneDatosUsuarioJsonResponse
        {
            public ObtieneDatosUsuarioResponse ObtieneDatosUsuarioResult { get; set; }
        }
    }
}
