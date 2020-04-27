using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{
    public class Comun
    {
        public string EnvioPeticionRest(string json, string url)
        {
            string Resultado = string.Empty;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
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

        public string obtieneFoto(string file, IConfiguration _configuration)
        {
            StructJson json = new StructJson();
            json.Request.filePath = Path.Combine(file);
            string[] ext = Path.GetFileName(Path.Combine(file)).Split('.');
            FileToBase64JsonResponse jsonResult = JsonConvert.DeserializeObject<FileToBase64JsonResponse>(EnvioPeticionRest(JsonConvert.SerializeObject(json, Formatting.Indented), _configuration.GetValue<string>("appSettings:fileToBase64")));
            return "data:image/" + ext[1] + ";base64," + jsonResult.fileToBase64RestResult.base64String;
        }
        public class StructJson { public StructJsonRequest Request { get; set; } public StructJson() { this.Request = new StructJsonRequest(); } }
        public class StructJsonRequest { public string filePath { get; set; } }
        public class FileToBase64RestResult { public string base64String { get; set; } }
        public class FileToBase64JsonResponse { public FileToBase64RestResult fileToBase64RestResult { get; set; } }
    }
}
