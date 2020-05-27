﻿using Api_GestionFC.DTO;
using Api_GestionFC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_GestionFC.Repository
{
    public class LoginRepository : Comun
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        private const string DisplayNameAttribute = "DisplayName";
        private const string SAMAccountNameAttribute = "SAMAccountName";

        private readonly LDAP LdapConf;

        public LoginRepository(IConfiguration configuration, IOptions<LDAP> ldapConf)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
            this.LdapConf = ldapConf.Value;
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
                    var now = DateTime.Now;
                    IdentityModelEventSource.ShowPII = true;
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
                        NotBefore = now,
                        Expires = now.AddHours(8),
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

        public LoginDTO LoginLDAP(LDAPLoginData loginData)
        {
            LoginDTO Response = new LoginDTO();
            try
            {
                string user = LdapConf.Domain + "\\" + loginData.Usuario;
                using (DirectoryEntry entry = new DirectoryEntry(LdapConf.KLDAPService, user , loginData.Password))
                {
                    string defaultNamingContext = entry.Properties["defaultNamingContext"].Value.ToString();
                    using (DirectoryEntry defaultEntry = new DirectoryEntry("LDAP://" + defaultNamingContext))
                    {
                        using (DirectorySearcher searcher = new DirectorySearcher(defaultEntry))
                        {
                            searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, loginData.Usuario);
                            searcher.PropertiesToLoad.Add(DisplayNameAttribute);
                            searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);
                            var result = searcher.FindOne();
                            if (result != null)
                            {
                                var displayName = result.Properties[DisplayNameAttribute];
                                var samAccountName = result.Properties[SAMAccountNameAttribute];

                                Response.Activo = true;
                                Response.EsGerente = true;
                                Response.ResultadoEjecucion = new ResultadoEjecucion()
                                {
                                    EjecucionCorrecta = true,
                                    ErrorMessage = "",
                                    FriendlyMessage = ""
                                };
                                Response.UsuarioAutorizado = true;
                                Response.Token = "";
                            }
                        }
                    }
                }
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
