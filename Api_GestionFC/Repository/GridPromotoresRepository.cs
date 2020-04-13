using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO = Api_GestionFC.DTO;

namespace Api_GestionFC.Repository
{

    public class GridPromotoresRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
    
        public GridPromotoresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public async Task<DTO.GridPromotoresDTO> GetGridPromotores(int nomina)
        {
            var response = new DTO.GridPromotoresDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_Datos_Plantilla", sqlConn))
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
                                    response.Promotores.Add(new Models.Progreso { 
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellidos = reader["Apellidos"].ToString(),
                                        Foto = reader["Foto"].ToString(),
                                        Genero = reader["Genero"].ToString(),
                                        ColorIndicadorMeta = reader["ColorIndicadorMeta"].ToString(),
                                        SaldoVirtual = reader["SaldoVirtual"].ToString(),
                                        SaldoCantadoFCT = reader["SaldoCantadoFCT"].ToString(),
                                        SaldoAcumulado = reader["SaldoAcumulado"].ToString(),
                                        PorcentajeSaldoAcumulado = reader["PorcentajeSaldoAcumulado"].ToString(),
                                        PorcentajeSaldoVirtual = reader["PorcentajeSaldoVirtual"].ToString(),
                                        FCTInactivos = Convert.ToInt32(reader["FCTInactivos"]),
                                        TramitesCertificados = Convert.ToInt32(reader["TramitesCertificados"])
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
