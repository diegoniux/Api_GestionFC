﻿using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Api_GestionFC.Repository
{
    public class AlertaRepository : Comun
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public AlertaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public async Task<DTO.AlertaImproductividadDTO> GetAlertaImproductividad(int nomina, int NominaAP = 0)
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
                        if (NominaAP != 0)
                        {
                            sqlCmd.Parameters.AddWithValue("@p_NominaAP", NominaAP);
                        }

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
                                        Apellidos = reader["Apellidos"].ToString(),
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
                                        Apellidos = reader["Apellidos"].ToString(),
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

        public async Task<DTO.AlertaRecuperacionDTO> GetAlertaRecuperacion(int nomina)
        {
            var response = new DTO.AlertaRecuperacionDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_GenerarAlertasPlantillaRecuperacion", sqlConn))
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
                                    response.ResultDatos.Add(new Models.AlertaRecuperacion
                                    {
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        IdAlerta = Convert.ToInt32(reader["IdAlerta"]),
                                        IdTipoAlerta = Convert.ToInt32(reader["IdTipoAlerta"]),
                                        IdEstatusAlerta = Convert.ToInt32(reader["IdEstatusAlerta"]),
                                        NominaAP = Convert.ToInt32(reader["NominaAP"]),
                                        NombreAP = reader["NombreAP"].ToString(),
                                        ApellidosAP = reader["ApellidosAP"].ToString(),
                                        ValidacionesRecuperacion = Convert.ToInt32(reader["ValidacionesRecuperacion"]),
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

        public async Task<DTO.AlertaInvestigacionDTO> GetAlertaInvestigacion(int nomina)
        {
            var response = new DTO.AlertaInvestigacionDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_GenerarAlertasPlantillaInvestigacion", sqlConn))
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
                                    response.ResultDatos.Add(new Models.AlertaInvestigacion
                                    {
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        IdAlerta = Convert.ToInt32(reader["IdAlerta"]),
                                        IdTipoAlerta = Convert.ToInt32(reader["IdTipoAlerta"]),
                                        IdEstatusAlerta = Convert.ToInt32(reader["IdEstatusAlerta"]),
                                        NominaAP = Convert.ToInt32(reader["NominaAP"]),
                                        NombreAP = reader["NombreAP"].ToString(),
                                        ApellidosAP = reader["ApellidosAP"].ToString(),
                                        ValidacionesInvestigacion = Convert.ToInt32(reader["ValidacionesInvestigacion"]),
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
        public async Task<DTO.FoliosRecuperacionDTO> GetFoliosRecuperacion(int nomina)
        {
            var response = new DTO.FoliosRecuperacionDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Sps_Reporte_TramitesRecuperacion", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_UsuarioId", nomina);

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
                                    response.ListadoFolios.Add(new Models.FolioSolicitud
                                    {
                                        Folio = reader["FolioSolicitud"].ToString(),
                                        RegistroTraspasoId = Convert.ToInt32(reader["RegistroTraspasoId"])
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

        public async Task<DTO.AlertaRecuperacionDTO> GetDetalleFolioRecuperacion(int RegistroTraspasoId, int PantallaId)
        {
            var response = new DTO.AlertaRecuperacionDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_Detalle_FolioRecuperacion", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_RegistroTraspasoId", RegistroTraspasoId);
                        sqlCmd.Parameters.AddWithValue("@p_PantallaId", PantallaId);

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
                                string documento;
                                string Path;
                                int Len;
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.Pantallas.Add(new Models.AlertaRecuperacionPantallas
                                    {
                                        PantallaId = Convert.ToInt32(reader["PantallaId"]),
                                        PantallaDesc = reader["PantallaDesc"].ToString()
                                    });
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.Preguntas.Add(new Models.AlertaRecuperacionPreguntas
                                    {
                                        PreguntaId = Convert.ToInt32(reader["PreguntaId"]),
                                        PreguntaDesc = reader["PreguntaDesc"].ToString()
                                    });
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    Path = reader["Path"].ToString();
                                    Len = Path.Length;
                                    if(Path.Substring(Len - 1,1) == @"\")
                                    {
                                        documento = Path + reader["Mascara"].ToString();
                                    }
                                    else
                                    {
                                        documento = Path + @"\" + reader["Mascara"].ToString();
                                    }
                                    documento = documento.Replace("\\\\", "\\");

                                    response.Documentos.Add(new Models.AlertaRecuperacionDocumentos
                                    {
                                        Pantalla = Convert.ToInt32(reader["Pantalla"]),
                                        ExpedienteTipoId = Convert.ToInt32(reader["ExpedienteTipoId"]),
                                        ExpedienteDesc = reader["ExpedienteDesc"].ToString(),
                                        DocumentoTipoId = Convert.ToInt32(reader["DocumentoTipoId"]),
                                        DocumentoDesc = reader["DocumentoDesc"].ToString(),
                                        ClaveDocumento = reader["ClaveDocumento"].ToString(),
                                        Consecutivo = Convert.ToInt32(reader["Consecutivo"]),
                                        Mascara = obtieneFoto(documento, _configuration),
                                        Path = reader["Path"].ToString()
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
        public async Task<DTO.AlertaMensajeDTO> GetMensajeGerente(int nomina)
        {
            var response = new DTO.AlertaMensajeDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_ObtieneMensaje_Banner", sqlConn))
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
                                    response.Mensaje = reader["Mensaje"].ToString();
                                    response.SaldoAcomuladoMeta = Convert.ToDecimal(reader["SaldoAcomuladoMeta"]).ToString("C0");
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
