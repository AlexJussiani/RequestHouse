using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using RH.Core.Messages;
using System.Text;
using RH.Core.Communication;

namespace RH.ApiGateways.Services
{
    public abstract class Service: CommandHandler
    {
        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }
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

        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }

        protected ResponseResult RetornoValidation()
        {
            var response = new ResponseResult();
            foreach (var validation in ValidationResult.Errors)
            {
                response.Errors.Mensagens.Add(validation.ErrorMessage);
            }
            return response;
        }
    }
}
