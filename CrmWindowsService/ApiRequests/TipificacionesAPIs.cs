using System.Collections.Generic;
using System.Net.Http;
using System;
using System.Threading.Tasks;
// using Microsoft.Extensions.Configuration;
using System.Configuration;
using Newtonsoft.Json;
using Serilog;
using CrmWindowsService.ApiRequests.Models;

namespace CrmWindowsService.ApiRequests
{

    public class TipificacionesAPIs
    {
        private readonly string API_PATH;
        private readonly string API_KEY;
        private readonly int TIMEOUT;

        //private readonly string API_PATH = "https://intcrmdyn365ucts.continental.edu.pe/ApiD365/api";
        //private readonly string API_KEY = "J274bMdKCjx/J3JkHdglXGOjs3NpnMz5k3EfjuQCyZw=";
        //private readonly int TIMEOUT = 10000;

        public TipificacionesAPIs()
        {
            // Leer configuración desde app.config
            API_PATH = ConfigurationManager.AppSettings["ApiPath"];
            API_KEY = ConfigurationManager.AppSettings["ApiKey"];
            TIMEOUT = Convert.ToInt32(ConfigurationManager.AppSettings["Timeout"]);

            Log.Information($"API_PATH: {API_PATH}");
            Log.Information($"API_KEY: {API_KEY}");
            Log.Information($"TIMEOUT: {TIMEOUT}");

            //IConfiguration config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            //API_PATH = config.GetSection("APIConfig")["ApiPath"];
            //API_KEY = config.GetSection("APIConfig")["ApiKey"];
            //TIMEOUT = Convert.ToInt32(config.GetSection("APIConfig")["Timeout"]);
        }

        public List<ConsultarConfiguracionTipificacion> getDataTipificaciones()
        {
            string apiUrl = $"{API_PATH}/Caso/Catalogos/ConsultarConfiguracionTipificacion";
            string apiKey = API_KEY;

            var generalResponse = GeneralGetRequest<List<ConsultarConfiguracionTipificacion>>(apiUrl, apiKey);
            generalResponse.Wait();

            GeneralResponse<List<ConsultarConfiguracionTipificacion>> response = generalResponse.Result;

            if (response == null || response.IndicadorExito == "NOK")
            {
                Log.Error($"Error en respuesta de API: {response?.DescripcionError}");

                return null;
            }

            return response.Detail;
        }

        private async Task<GeneralResponse<T>> GeneralGetRequest<T>(string api_url, string api_key)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (HttpRequestMessage, cert, certChain, policyErrors) =>
                {
                    return true;
                };
            try
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromSeconds(TIMEOUT);
                    client.DefaultRequestHeaders.Add("Api_Key", api_key);

                    Log.Information("================== Nueva Consulta a API ==================");
                    Log.Information($"API_URL: {api_url}");
                    Log.Information($"API_KEY: {api_key}");

                    HttpResponseMessage response = await client.GetAsync(api_url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        GeneralResponse<T> generalResponse = JsonConvert.DeserializeObject<GeneralResponse<T>>(jsonContent);

                        if (generalResponse.IndicadorExito == "OK")
                        {
                            Log.Information($"Data Recibida de la API: ----> OK");
                        }
                        else
                        {
                            Log.Error($"Error: {generalResponse.DescripcionError}");
                        }

                        return generalResponse;
                    }

                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            return null;
        }
    }
}
