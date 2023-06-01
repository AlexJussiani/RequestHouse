using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace RH.ApiGateways.Services
{
    public abstract class Service
    {
        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var teste = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }
        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
