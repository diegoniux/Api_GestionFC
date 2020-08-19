using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{
    public class DetalleEspecialistaRepository : Comun
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DetalleEspecialistaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDBDEV");
            this._configuration = configuration;
        }

        public async Task<DTO.DetalleEspecialistaDTO> GetDetalleEspecialista(int nominaAP, int nomina)
        {
            var response = new DTO.DetalleEspecialistaDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_ConsultarDetalleEspecialista", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@p_NominaAP", nominaAP);
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
                                    foto = reader["Foto"].ToString();
                                    response.detalleEspecialista.NominaPromotor = Convert.ToInt32(reader["NominaPromotor"]);
                                    response.detalleEspecialista.Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration);
                                    response.detalleEspecialista.Genero = reader["Genero"].ToString();
                                    response.detalleEspecialista.Nombre = reader["Nombre"].ToString();
                                    response.detalleEspecialista.Apellidos = reader["Apellidos"].ToString();
                                    response.detalleEspecialista.Telefono = reader["Telefono"].ToString();
                                    response.detalleEspecialista.MesesLaborando = reader["MesesLaborando"].ToString();
                                    response.detalleEspecialista.SaldoAcumulado = reader["SaldoAcumulado"].ToString();
                                    response.detalleEspecialista.SaldoMeta = reader["SaldoMeta"].ToString();
                                    response.detalleEspecialista.PorcentajeSaldoAcumulado = reader["PorcentajeSaldoAcumulado"].ToString();
                                    response.detalleEspecialista.ImagenSaldoAcumulado = reader["ImagenSaldoAcumulado"].ToString();
                                    response.detalleEspecialista.NivelComision = Convert.ToInt32(reader["NivelComision"]);
                                    response.detalleEspecialista.TramitesPorRecuperar = Convert.ToInt32(reader["TramitesPorRecuperar"]);
                                    response.detalleEspecialista.TramitesEnCalidad = Convert.ToInt32(reader["TramitesEnCalidad"]);
                                }

                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.desafios.BonoTableta = Convert.ToInt32(reader["BonoTableta"]);
                                    response.desafios.ImgBonoTableta = reader["ImgBonoTableta"].ToString();
                                    response.desafios.ColorBonoTableta = reader["ColorBonoTableta"].ToString();
                                    response.desafios.Miltiplicador = Convert.ToInt32(reader["Miltiplicador"]);
                                    response.desafios.ImgMultiplicador = reader["ImgMultiplicador"].ToString();
                                    response.desafios.ColorMultiplicador = reader["ColorMultiplicador"].ToString();
                                    response.desafios.BonoBisemanal = Convert.ToInt32(reader["BonoBisemanal"]);
                                    response.desafios.ImgBonoBisemanal = reader["ImgBonoBisemanal"].ToString();
                                    response.desafios.ColorBonoBisemanal = reader["ColorBonoBisemanal"].ToString();
                                    response.desafios.Prospectos = Convert.ToInt32(reader["Prospectos"]);
                                    response.desafios.ImgProspectos = reader["ImgProspectos"].ToString();
                                    response.desafios.ColorProspectos = reader["ColorProspectos"].ToString();
                                    response.desafios.MetaComercial = Convert.ToInt32(reader["MetaComercial"]);
                                    response.desafios.ImgMetaComercial = reader["ImgMetaComercial"].ToString();
                                    response.desafios.ColorMetaComercial = reader["ColorMetaComercial"].ToString();
                                    response.desafios.SemanasMeta = Convert.ToInt32(reader["SemanasMeta"]);
                                    response.desafios.ImgSemanasMeta = reader["ImgSemanasMeta"].ToString();
                                    response.desafios.ColorSemanasMeta = reader["ColorSemanasMeta"].ToString();
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

        public async Task<DTO.DetalleFolioDTO> GetDetalleFolio(string folioSolicitud)
        {
            var response = new DTO.DetalleFolioDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_ConsultarDetalleFolio", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@paFolioSolicitud", folioSolicitud);

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
                             
                                    response.detalleFolios.RegistroTraspasoId = Convert.ToInt32(reader["RegistroTraspasoId"]);
                                    response.detalleFolios.FolioSolicitud = reader["FolioSolicitud"].ToString();
                                    response.detalleFolios.EstatusId = Convert.ToInt32(reader["EstatusId"]);
                                    response.detalleFolios.EstatusDescripcion = reader["EstatusDescripcion"].ToString();
                                    response.detalleFolios.Seccion = Convert.ToInt32(reader["Seccion"]);
                                }

                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.detalleEtapas.RegistroTraspasoId = Convert.ToInt32(reader["RegistroTraspasoId"]);
                                    response.detalleEtapas.EtapaId = Convert.ToInt32(reader["EtapaId"]);
                                    response.detalleEtapas.EtapaDescripcion = reader["EtapaDescripcion"].ToString();
                                    response.detalleEtapas.Usuario = reader["Usuario"].ToString();
                                    response.detalleEtapas.Fecha = reader["Fecha"].ToString();
                                    response.detalleEtapas.Hora = reader["Hora"].ToString();
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

        public async Task<DTO.DetalleHistoricoDTO> GetDetalleEspecialistaHistorico(int nomina, DateTime Fecha, bool Posterior)
        {
            var response = new DTO.DetalleHistoricoDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_DetalleEspecialista_Historico", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);
                        sqlCmd.Parameters.AddWithValue("@p_Fecha", Fecha);
                        sqlCmd.Parameters.AddWithValue("@p_Posterior", Posterior);

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

                                    response.detalleHistoricoHeader.HabilitarAdelantar = Convert.ToBoolean(reader["HabilitarAdelantar"]);
                                    response.detalleHistoricoHeader.FechaIniFin = reader["FolioSolicitud"].ToString();
                                    response.detalleHistoricoHeader.HabilitarAnterior = Convert.ToBoolean(reader["HabilitarAnterior"]);
                                }

                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.detalleHistoricoSemanas.FechaSemana1 = reader["FechaSemana1"].ToString();
                                    response.detalleHistoricoSemanas.FechaSemana2 = reader["FechaSemana2"].ToString();
                                    response.detalleHistoricoSemanas.FechaSemana3 = reader["FechaSemana3"].ToString();
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.detalleHistoricoTramites.Add(new Models.DetalleHistoricoTramitesRecup
                                    {
                                        Tramites = reader["Tramites"].ToString(),
                                        Semana1 = reader["Semana1"].ToString(),
                                        Semana2 = reader["Semana2"].ToString(),
                                        Semana3 = reader["Semana3"].ToString()
                                    });
                                }

                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.detalleHistoricoRecuperacion.Add(new Models.DetalleHistoricoTramitesRecup
                                    {
                                        Tramites = reader["Recuperaciones"].ToString(),
                                        Semana1 = reader["Semana1"].ToString(),
                                        Semana2 = reader["Semana2"].ToString(),
                                        Semana3 = reader["Semana3"].ToString()
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
