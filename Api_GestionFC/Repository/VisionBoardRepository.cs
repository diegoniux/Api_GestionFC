using Api_GestionFC.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{
    public class VisionBoardRepository: Comun
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public VisionBoardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public async Task<MetaPlantillaDTO> GetMetaPlantilla(int nomina)
        {
            var response = new MetaPlantillaDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_ConsultarMetaPlantilla", sqlConn))
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
                                    response.MetaPlantilla = new Models.MetaPlantilla()
                                    {
                                        IdMetaSaldoAcumuladoGerenteIndividual = (int)reader["IdMetaSaldoAcumuladoGerenteIndividual"],
                                        ExisteConfiguracionIndividual = (bool)reader["ExisteConfiguracionIndividual"],
                                        SaldoAcumuladoMeta = reader["SaldoAcumuladoMeta"].ToString(),
                                        SaldoAcumuladoTetra = reader["SaldoAcumuladoTetra"].ToString(),
                                        Traspasos = (int)reader["Traspasos"],
                                        TraspasosFCT = (int)reader["TraspasosFCT"],
                                        ComisionSem = reader["ComisionSem"].ToString(),
                                        BonoDesarrollo = reader["BonoDesarrollo"].ToString(),
                                        BonoExcelencia = reader["BonoExcelencia"].ToString(),
                                        SemanaTetraSemana = (int)reader["SemanaTetraSemana"],
                                        MaxSemanas = (int)reader["MaxSemanas"],
                                        TotalTetra = reader["TotalTetra"].ToString()
                                    };
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.FechasSemana = new Models.FechasSemana()
                                    {
                                        FechaLunes = reader["FechaLunes"].ToString(),
                                        FechaMartes = reader["FechaMartes"].ToString(),
                                        FechaMiercoles = reader["FechaMiercoles"].ToString(),
                                        FechaJueves = reader["FechaJueves"].ToString(),
                                        FechaViernes = reader["FechaViernes"].ToString(),
                                        FechaSabado = reader["FechaSabado"].ToString(),
                                        FechaDomingo = reader["FechaDomingo"].ToString()
                                    };
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.DetalleMetaPorDia = new Models.DetalleMetaPorDia()
                                    {
                                        SaldoLunes = (int)reader["SaldoLunes"],
                                        SaldoMartes = (int)reader["SaldoMartes"],
                                        SaldoMiercoles = (int)reader["SaldoMiercoles"],
                                        SaldoJueves = (int)reader["SaldoJueves"],
                                        SaldoViernes = (int)reader["SaldoViernes"],
                                        SaldoSabado = (int)reader["SaldoSabado"],
                                        SaldoDomingo = (int)reader["SaldoDomingo"]
                                    };
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

        public async Task<MetaPlantillaIndividualDTO> GetMetaPlantillaIndividual(int nomina)
        {
            var response = new MetaPlantillaIndividualDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_ConsultarMetaPlantillaIndividual", sqlConn))
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
                                    response.ComisionEstimada = reader["ComisionSemana"].ToString();
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    string foto = reader["Foto"].ToString();

                                    response.ListMetaAP.Add(new Models.MetaAP()
                                    {
                                        IdDetalleMetaSaldoAcumuladoAP = (int)reader["IdDetalleMetaSaldoAcumuladoAP"],
                                        Nomina = (int)reader["Nomina"],
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellidos = reader["Apellidos"].ToString(),
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        ComisionEstimada = reader["ComisionEstimada"].ToString(),
                                        SaldoMeta = reader["SaldoMeta"].ToString(),
                                        EsFrontera = (bool)reader["EsFrontera"],
                                        EsNovato = (bool)reader["EsNovato"]
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

        public async Task<MetaPlantillaResponseDTO> RegistrarMetaPlantilla(MetaPlantillaRequestDTO Request)
        {
            var response = new MetaPlantillaResponseDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_RegistrarMetaPlantilla", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_IdMetaSaldoAcumuladoGerenteIndividual", Request.MetaPlantillaDia.IdMetaSaldoAcumuladoGerenteIndividual);
                        sqlCmd.Parameters.AddWithValue("@p_IdPeriodo", Request.MetaPlantillaDia.IdPeriodo);
                        sqlCmd.Parameters.AddWithValue("@p_Nomina", Request.MetaPlantillaDia.Nomina);
                        sqlCmd.Parameters.AddWithValue("@p_SaldoAcumuladoMeta", Request.MetaPlantillaDia.SaldoAcumuladoMeta);
                        sqlCmd.Parameters.AddWithValue("@p_Dia", Request.MetaPlantillaDia.Dia);

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
                                while (await reader.ReadAsync())
                                {
                                    response.IdMetaSaldoAcumuladoGerenteIndividual = (int)reader["IdMetaSaldoAcumuladoGerenteIndividual"];
                                    response.SaldoAcumuladoMeta = (int)reader["SaldoAcumuladoMeta"];
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

        public async Task<MetaPlantillaIndividualResponseDTO> RegistrarMetaPlantillaIndividual(MetaPlantillaIndividualRequestDTO Request)
        {
            var response = new MetaPlantillaIndividualResponseDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_RegistrarMetaPlantillaIndividual", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_IdMetaSaldoAcumuladoGerenteIndividual", Request.MetaPlantillaIndividual.IdMetaSaldoAcumuladoGerenteIndividual);
                        sqlCmd.Parameters.AddWithValue("@p_NominaAP", Request.MetaPlantillaIndividual.NominaAP);
                        sqlCmd.Parameters.AddWithValue("@p_Nomina", Request.MetaPlantillaIndividual.Nomina);
                        sqlCmd.Parameters.AddWithValue("@p_SaldoAcumuladoMeta", Request.MetaPlantillaIndividual.SaldoAcumuladoMeta);

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
                                while (await reader.ReadAsync())
                                {
                                    response.IdMetaSaldoAcumuladoGerenteIndividual = (int)reader["IdMetaSaldoAcumuladoGerenteIndividual"];
                                    response.SaldoAcumuladoMeta = (int)reader["SaldoAcumuladoMeta"];
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

        public async Task<MetaPlantillaFoliosResponseDTO> RegistrarMetaPlantillaFolios(MetaPlantillaFoliosRequestDTO Request)
        {
            var response = new MetaPlantillaFoliosResponseDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_RegistrarMetaPlantillaFolios", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_IdMetaSaldoAcumuladoGerenteIndividual", Request.MetaPlantillaFolios.IdMetaSaldoAcumuladoGerenteIndividual);
                        sqlCmd.Parameters.AddWithValue("@p_Nomina", Request.MetaPlantillaFolios.Nomina);
                        sqlCmd.Parameters.AddWithValue("@p_Folios", Request.MetaPlantillaFolios.Folios);
                        sqlCmd.Parameters.AddWithValue("@p_FoliosFCT", Request.MetaPlantillaFolios.FoliosFCT);

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
                                while (await reader.ReadAsync())
                                {
                                    response.IdMetaSaldoAcumuladoGerenteIndividual = (int)reader["ComisionEstimada"];
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

        public async Task<MetaPlantillaComisionSemResponseDTO> RegistrarMetaPlantillaComisionSem(MetaPlantillaComisionSemRequestDTO Request)
        {
            var response = new MetaPlantillaComisionSemResponseDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_RegistrarMetaPlantillaComisionSem", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_IdMetaSaldoAcumuladoGerenteIndividual", Request.MetaComisionSem.IdMetaSaldoAcumuladoGerenteIndividual);
                        sqlCmd.Parameters.AddWithValue("@p_IdPeriodo", Request.MetaComisionSem.IdPeriodo);
                        sqlCmd.Parameters.AddWithValue("@p_Nomina", Request.MetaComisionSem.Nomina);
                        sqlCmd.Parameters.AddWithValue("@p_ComisionSem", Request.MetaComisionSem.ComisionSem);

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
                                while (await reader.ReadAsync())
                                {
                                    response.IdMetaSaldoAcumuladoGerenteIndividual = (int)reader["IdMetaSaldoAcumuladoGerenteIndividual"];
                                    response.SaldoAcumuladoMeta = (int)reader["SaldoAcumuladoMeta"];
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

        public async Task<MetaPlantillaSaldoAcumuladoResponseDTO> RegistrarMetaPlantillaSaldoAcumulado(MetaPlantillaSaldoAcumuladoRequestDTO Request)
        {
            var response = new MetaPlantillaSaldoAcumuladoResponseDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_RegistrarMetaPlantillaSaldoAcumulado", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_IdMetaSaldoAcumuladoGerenteIndividual", Request.MetaSaldoAcumulado.IdMetaSaldoAcumuladoGerenteIndividual);
                        sqlCmd.Parameters.AddWithValue("@p_IdPeriodo", Request.MetaSaldoAcumulado.IdPeriodo);
                        sqlCmd.Parameters.AddWithValue("@p_Nomina", Request.MetaSaldoAcumulado.Nomina);
                        sqlCmd.Parameters.AddWithValue("@p_SaldoAcumuladoMeta", Request.MetaSaldoAcumulado.SaldoAcumuladoMeta);

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
                                while (await reader.ReadAsync())
                                {
                                    response.IdMetaSaldoAcumuladoGerenteIndividual = (int)reader["IdMetaSaldoAcumuladoGerenteIndividual"];
                                    response.SaldoAcumuladoMeta = (int)reader["SaldoAcumuladoMeta"];
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
