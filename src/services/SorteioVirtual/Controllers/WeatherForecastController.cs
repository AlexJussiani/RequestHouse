using Correrio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorteioVirtual.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string categoriaServico(string cep)
        {
            var endereco = "";
            using (var ws = new AtendeClienteClient())
            {
                try
                {
                    endereco = ws.consultaCEPAsync(cep).ToString();
                }
                catch (System.Exception wx)
                {
                    throw;
                }
            }
            return endereco;
        }
    }
}
