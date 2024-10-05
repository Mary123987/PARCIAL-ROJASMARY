using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq; 

namespace PARCIAL_ROJASMARY.Controllers
{
    [Route("[controller]")]
    public class ConversorController : Controller
    {
        private readonly ILogger<ConversorController> _logger;
        private readonly HttpClient _httpClient;

        public ConversorController(ILogger<ConversorController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Convertir(decimal monto, string moneda)
        {
            decimal tasaCambio = await ObtenerTasaCambio(moneda);
            decimal montoFinal = monto * tasaCambio;

            ViewBag.MontoConvertido = montoFinal;
            ViewBag.MonedaDestino = moneda == "USD" ? "BTC" : "USD"; 
            return View("Conversor"); 
        }

private async Task<decimal> ObtenerTasaCambio(string moneda)
{
    string apiUrl = moneda == "USD" 
        ? "https://api.coindesk.com/v1/bpi/currentprice/BTC.json" 
        : "https://api.coindesk.com/v1/bpi/currentprice/USD.json";

    try
    {
        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(data);

            // Verificamos si el JSON contiene la estructura esperada
            var bpi = json["bpi"];
            if (bpi != null)
            {
                var tasa = moneda == "USD" ? bpi["BTC"]?["rate_float"] : bpi["USD"]?["rate_float"];
                if (tasa != null)
                {
                    return (decimal)tasa;
                }
            }
        }
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error al obtener la tasa de cambio: {ex.Message}");
    }
    return 1; // Devolver 1 si hay un error o no se encuentra la tasa de cambio
}


        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
