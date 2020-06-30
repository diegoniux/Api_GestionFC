using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{
    public class AlertaRepository : Comun
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public AlertaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDBDEV");
            this._configuration = configuration;
        }

        public async Task<DTO.AlertaImproductividadDTO> GetAlertaImproductividad(int nomina)
        {
            var response = new DTO.AlertaImproductividadDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_GenerarAlertasPlantillaImproductiva", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);

                        await sqlConn.OpenAsync();

                        using (var reader = await sqlCmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.ResultadoEjecucion.EjecucionCorrecta = Convert.ToBoolean(reader["EjecucionCorrecta"]);
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }

                            //Si la ejecución es exitosa                                 
                            if (response.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                string foto;
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    foto = reader["Foto"].ToString();
                                    response.ResultDatos.Add(new Models.AlertaImproductividad
                                    {
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        IdAlerta = Convert.ToInt32(reader["IdAlerta"]),
                                        IdTipoAlerta = Convert.ToInt32(reader["IdTipoAlerta"]),
                                        IdEstatusAlerta = Convert.ToInt32(reader["IdEstatusAlerta"]),
                                        NominaAP = Convert.ToInt32(reader["NominaAP"]),
                                        NombreAP = reader["NombreAP"].ToString(),
                                        ApellidosAP  = reader["ApellidosAP"].ToString(),
                                        DiasSinFolios = Convert.ToInt32(reader["DiasSinFolios"]),
                                        DiasRestantes = Convert.ToInt32(reader["DiasRestantes"]),
                                        Msj1 = reader["Msj1"].ToString(),
                                        Msj2 = reader["Msj2"].ToString(),
                                        Msj3 = reader["Msj3"].ToString(),
                                        BanderaCalendar = Convert.ToBoolean(reader["BanderaCalendar"]),
                                        ColorCalendar = reader["ColorCalendar"].ToString(),
                                        MsjEstatus = reader["MsjEstatus"].ToString(),
                                        ImgNotificacion = reader["ImgNotificacion"].ToString(),
                                        ImgWarning = Convert.ToBoolean(reader["ImgWarning"])
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }


        public async Task<DTO.AlertaSinSaldoVirtualDTO> GetAlertaSinSaldoVirtual(int nomina)
        {
            var response = new DTO.AlertaSinSaldoVirtualDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_GenerarAlertasFoliosSinSaldo", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);

                        await sqlConn.OpenAsync();

                        using (var reader = await sqlCmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.ResultadoEjecucion.EjecucionCorrecta = Convert.ToBoolean(reader["EjecucionCorrecta"]);
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }

                            //Si la ejecución es exitosa                                 
                            if (response.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultDatos.Add(new Models.AlertaSinSaldoVirtual
                                    {
                                        IdAlerta = Convert.ToInt32(reader["IdAlerta"]),
                                        IdTipoAlerta = Convert.ToInt32(reader["IdTipoAlerta"]),
                                        IdEstatusAlerta = Convert.ToInt32(reader["IdEstatusAlerta"]),
                                        Nombre = reader["Nombre"].ToString(),
                                        Folio = reader["Folio"].ToString(),
                                        SaldoVirtual = reader["SaldoVirtual"].ToString(),
                                        TipoSolicitud = reader["TipoSolicitud"].ToString(),
                                        FechaFirma = reader["FechaFirma"].ToString(),
                                        TieneSV = Convert.ToBoolean(reader["TieneSV"])
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public async Task<DTO.AlertaSinSaldoVirtualDTO> GetAlertaSeguimientoSinSaldoVirtual(int nomina,int IdAlerta)
        {
            var response = new DTO.AlertaSinSaldoVirtualDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_SeguimientoAlertasFolioSaldo", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);
                        sqlCmd.Parameters.AddWithValue("@p_IdAlerta", IdAlerta);

                        await sqlConn.OpenAsync();

                        using (var reader = await sqlCmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.ResultadoEjecucion.EjecucionCorrecta = Convert.ToBoolean(reader["EjecucionCorrecta"]);
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }

                            //Si la ejecución es exitosa                                 
                            if (response.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultDatos.Add(new Models.AlertaSinSaldoVirtual
                                    {
                                        IdAlerta = Convert.ToInt32(reader["IdAlerta"]),
                                        IdTipoAlerta = Convert.ToInt32(reader["IdTipoAlerta"]),
                                        IdEstatusAlerta = Convert.ToInt32(reader["IdEstatusAlerta"]),
                                        Nombre = reader["Nombre"].ToString(),
                                        Folio = reader["Folio"].ToString(),
                                        SaldoVirtual = reader["SaldoVirtual"].ToString(),
                                        TipoSolicitud = reader["TipoSolicitud"].ToString(),
                                        FechaFirma = reader["FechaFirma"].ToString(),
                                        TieneSV = Convert.ToBoolean(reader["TieneSV"])
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
