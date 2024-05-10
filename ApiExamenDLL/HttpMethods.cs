using APIExamen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiExamenDLL
{
    public class HttpMethods
    {
        private static HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:44385/api/examen"),
        };
        public static async Task<List<tblExamen>> WSConsultarAsync(FiltroBusqueda filtros)
        {
            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}?Nombre={filtros.Nombre}&Descripcion={filtros.Descripcion}");
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<tblExamen>>(responseBody);
        }
        public static async Task<ApiResponse> WSAgregarAsync(tblExamen examen)
        {
            var json = JsonConvert.SerializeObject(examen);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{httpClient.BaseAddress}", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        }
        public static async Task<ApiResponse> WSActualizarAsync(tblExamen examen)
        {
            var json = JsonConvert.SerializeObject(examen);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{httpClient.BaseAddress}", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        }
        public static async Task<ApiResponse> WSEliminarAsync(tblExamen examen)
        {
            var response = await httpClient.DeleteAsync($"{httpClient.BaseAddress}/{examen.IdExamen}");
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        }
    }
}
