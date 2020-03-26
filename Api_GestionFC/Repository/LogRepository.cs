using Api_GestionFC.DTO;
using Api_GestionFC.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Api_GestionFC.Repository
{
    public class LogRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public LogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }


        public async Task<LogSistemaDTO> LogSistema(LogSistema logSistema)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spi_LogSistema", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_IdAccion", logSistema.IdAccion);
                        sqlCmd.Parameters.AddWithValue("@p_IdPantalla", logSistema.IdPantalla);
                        sqlCmd.Parameters.AddWithValue("@p_Usuario", logSistema.Usuario);
                        sqlCmd.Parameters.AddWithValue("@p_Dispositivo", logSistema.Dispositivo);


                        await sqlConn.OpenAsync();

                        var response = new LogSistemaDTO();
                        using (var reader = await sqlCmd.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();
                            response = new LogSistemaDTO()
                            { 
                                LogSistema = new LogSistema(),
                                ResultadoEjecucion = new ResultadoEjecucion()
                            };


                            response.ResultadoEjecucion.EjecucionCorrecta = (bool)reader["EjecucionCorrecta"];
                            //Si la ejecución es exitosa
                            if (response.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }
                            else
                            {
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }
                        }

                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                return new LogSistemaDTO()
                {
                    LogSistema = new LogSistema(),
                    ResultadoEjecucion = new ResultadoEjecucion
                    {
                        EjecucionCorrecta = false,
                        ErrorMessage = ex.Message,
                        FriendlyMessage = "Ocurrió un error interno."
                    }
                };
            }
        }

        public async Task<LogErrorDTO> LogError(LogError logError)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spi_LogErrores", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_IdPantalla", logError.IdPantalla);
                        sqlCmd.Parameters.AddWithValue("@p_Usuario", logError.Usuario);
                        sqlCmd.Parameters.AddWithValue("@p_Error", logError.Error);
                        sqlCmd.Parameters.AddWithValue("@p_Dispositivo", logError.Dispositivo);


                        await sqlConn.OpenAsync();

                        var response = new LogErrorDTO();
                        using (var reader = await sqlCmd.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();
                            response = new LogErrorDTO()
                            {
                                LogError = new LogError(),
                                ResultadoEjecucion = new ResultadoEjecucion()
                            };


                            response.ResultadoEjecucion.EjecucionCorrecta = (bool)reader["EjecucionCorrecta"];
                            //Si la ejecución es exitosa
                            if (response.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }
                            else
                            {
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }
                        }

                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                return new LogErrorDTO()
                {
                    LogError = new LogError(),
                    ResultadoEjecucion = new ResultadoEjecucion
                    {
                        EjecucionCorrecta = false,
                        ErrorMessage = ex.Message,
                        FriendlyMessage = "Ocurrió un error interno."
                    }
                };
            }
        }

    }
}
