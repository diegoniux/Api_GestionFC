using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{
    public class ProductividadRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public ProductividadRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public async Task<DTO.ProductividadDiariaDTO> GetProductividadDiaria(int nomina, int Anio, int SemanaAnio, DateTime? FechaCorte, bool? EsPosterior = false)
        {
            var response = new DTO.ProductividadDiariaDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_CalcularProductividadDiaria", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);
                        if (Anio > 0)
                            sqlCmd.Parameters.AddWithValue("@p_Anio", Anio);
                        if (SemanaAnio > 0)
                            sqlCmd.Parameters.AddWithValue("@p_SemanaAnio", SemanaAnio);
                        if (FechaCorte != Convert.ToDateTime("1900-01-01"))
                        {
                            sqlCmd.Parameters.AddWithValue("@p_FechaCorte", FechaCorte);
                            sqlCmd.Parameters.AddWithValue("@p_EsPosterior", EsPosterior);
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
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultFechas.FechaLunes = reader["FechaLunes"].ToString();
                                    response.ResultFechas.FechaMartes = reader["FechaMartes"].ToString();
                                    response.ResultFechas.FechaMiercoles = reader["FechaMiercoles"].ToString();
                                    response.ResultFechas.FechaJueves = reader["FechaJueves"].ToString();
                                    response.ResultFechas.FechaViernes = reader["FechaViernes"].ToString();
                                    response.ResultFechas.FechaSabado = reader["FechaSabado"].ToString();
                                    response.ResultFechas.FechaDomingo = reader["FechaDomingo"].ToString();
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultDatos.Add(new Models.ProductividadDiariaDatos
                                    {
                                        NombreCompletoAP = reader["NombreCompletoAP"].ToString(),
                                        SaldoVirtualLunes = Convert.ToInt32(reader["SaldoVirtualLunes"]),
                                        SaldoVirtualMartes = Convert.ToInt32(reader["SaldoVirtualMartes"]),
                                        SaldoVirtualMiercoles = Convert.ToInt32(reader["SaldoVirtualMiercoles"]),
                                        SaldoVirtualJueves = Convert.ToInt32(reader["SaldoVirtualJueves"]),
                                        SaldoVirtualViernes = Convert.ToInt32(reader["SaldoVirtualViernes"]),
                                        SaldoVirtualSabado = Convert.ToInt32(reader["SaldoVirtualSabado"]),
                                        SaldoVirtualDomingo = Convert.ToInt32(reader["SaldoVirtualDomingo"]),
                                        SaldoVirtualSemana = Convert.ToInt32(reader["SaldoVirtualSemana"]),
                                        IndicadorSaldoMeta = reader["IndicadorSaldoMeta"].ToString(),
                                        SaldoVirtualFaltante = Convert.ToInt32(reader["SaldoVirtualFaltante"]),
                                        MetaSemana = Convert.ToInt32(reader["MetaSemana"]),
                                        FCTInactivos = Convert.ToInt32(reader["FCTInactivos"]),
                                        FCTActivos = Convert.ToInt32(reader["FCTActivos"]),
                                        FoliosMenores30k = Convert.ToInt32(reader["FoliosMenores30k"]),
                                        FoliosCertificados = Convert.ToInt32(reader["FoliosCertificados"])
                                    });
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultAnioSemana.Anio = Convert.ToInt32(reader["Anio"]);
                                    response.ResultAnioSemana.EsActual = Convert.ToBoolean(reader["EsActual"]);
                                    response.ResultAnioSemana.SemanaAnio = Convert.ToInt32(reader["SemanaAnio"]);
                                    response.ResultAnioSemana.FechaCorte = Convert.ToDateTime(reader["FechaCorte"]);
                                    response.ResultAnioSemana.EsUltimaFechaCorte = Convert.ToBoolean(reader["EsUltimaFechaCorte"]);
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

        public async Task<DTO.ComisionEstimadaDTO> GetComisionEstimada(int nomina, DateTime Fecha)
        {
            var response = new DTO.ComisionEstimadaDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_CalcularComisionGerente", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);
                        if (Fecha != new DateTime())
                            sqlCmd.Parameters.AddWithValue("@p_Fecha", Fecha);
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
                                    response.BonoExcelenciaEstimado = reader["BonoExcelenciaEstimado"].ToString();
                                    response.ComisionEstimada = reader["ComisionEstimada"].ToString();
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

        public async Task<DTO.ProductividadSemanalDTO> GetProductividadSemanal(int nomina, int Anio, int TetrasemanaAnio, DateTime? FechaCorte , bool? EsPosterior = false)
        {
            var response = new DTO.ProductividadSemanalDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_CalcularProductividadSemanal", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);
                        if (Anio > 0)
                            sqlCmd.Parameters.AddWithValue("@p_Anio", Anio);
                        if (TetrasemanaAnio > 0)
                            sqlCmd.Parameters.AddWithValue("@p_TetrasemanaAnio", TetrasemanaAnio);
                        if (FechaCorte != Convert.ToDateTime("1900-01-01"))
                        {
                            sqlCmd.Parameters.AddWithValue("@p_FechaCorte", FechaCorte);
                            sqlCmd.Parameters.AddWithValue("@p_EsPosterior", EsPosterior);
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
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultSemanas.Semana1 = reader["Semana1"].ToString();
                                    response.ResultSemanas.Semana2 = reader["Semana2"].ToString();
                                    response.ResultSemanas.Semana3 = reader["Semana3"].ToString();
                                    response.ResultSemanas.Semana4 = reader["Semana4"].ToString();
                                }

                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultDatos.Add(new Models.ProductividadSemanalDatos
                                    {
                                        NombreCompletoAP = reader["NombreCompletoAP"].ToString(),
                                        SaldoVirtualSemana1 = Convert.ToInt32(reader["SaldoVirtualSemana1"]),
                                        SaldoVirtualSemana2 = Convert.ToInt32(reader["SaldoVirtualSemana2"]),
                                        SaldoVirtualSemana3 = Convert.ToInt32(reader["SaldoVirtualSemana3"]),
                                        SaldoVirtualSemana4 = Convert.ToInt32(reader["SaldoVirtualSemana4"]),
                                        IndicadorSaldoMetaSem1 = reader["IndicadorSaldoMetaSem1"].ToString(),
                                        IndicadorSaldoMetaSem2 = reader["IndicadorSaldoMetaSem2"].ToString(),
                                        IndicadorSaldoMetaSem3 = reader["IndicadorSaldoMetaSem3"].ToString(),
                                        IndicadorSaldoMetaSem4 = reader["IndicadorSaldoMetaSem4"].ToString(),
                                        SaldoVirtualTetrasemana = Convert.ToInt32(reader["SaldoVirtualTetrasemana"]),
                                        IndicadorSaldoMetaTetra = reader["IndicadorSaldoMetaTetra"].ToString()
                                    });
                                }

                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultTotal.SaldoVirtualTotal = Convert.ToInt32(reader["SaldoVirtualTotal"]);
                                    response.ResultTotal.Anio = Convert.ToInt32(reader["Anio"]);
                                    response.ResultTotal.TetrasemanaAnio = Convert.ToInt32(reader["TetrasemanaAnio"]);
                                    response.ResultTotal.EsActual = Convert.ToBoolean(reader["EsActual"]);
                                    response.ResultTotal.FechaCorte = Convert.ToDateTime(reader["FechaCorte"]);
                                    response.ResultTotal.EsUltimaFechaCorte = Convert.ToBoolean(reader["EsUltimaFechaCorte"]);
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
