using Microsoft.AspNetCore.Mvc;
using MvcGoogleMaps.Data;
using MvcGoogleMaps.Models;
using Nominatim.API.Geocoders;
using Nominatim.API.Models;
using Nominatim.API.Web;

namespace MvcGoogleMaps.Controllers
{
    public class GoogleMapsController : Controller
    {
        private readonly GoogleMapsContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public GoogleMapsController(GoogleMapsContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // GET: Muestra la pantalla inicial con el mapa por defecto
        public IActionResult Index()
        {
            // Le pasamos unas coordenadas por defecto para que el mapa no salga gris
            // (Ejemplo: Centro de Madrid)
            var sucursalInicial = new Sucursal
            {
                Nombre = "",
                DireccionTexto = "",
                Latitud = 40.4168,
                Longitud = -3.7038
            };

            return View(sucursalInicial);
        }

        // POST: Recibe el formulario, busca las coordenadas y actualiza el mapa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Sucursal sucursal)
        {
            if (!ModelState.IsValid) return View(sucursal);

            // 1. Buscamos en OpenStreetMap
            var nominatimWebInterface = new NominatimWebInterface(_httpClientFactory);
            var geocoder = new ForwardGeocoder(nominatimWebInterface, null);
            var request = new ForwardGeocodeRequest { queryString = sucursal.DireccionTexto };
            var response = await geocoder.Geocode(request);

            // 2. Si encuentra la dirección
            if (response != null && response.Length > 0)
            {
                sucursal.Latitud = response[0].Latitude;
                sucursal.Longitud = response[0].Longitude;

                // Guardamos en la Base de Datos
                _context.Sucursales.Add(sucursal);
                await _context.SaveChangesAsync();

                // Enviamos un mensaje de éxito a la vista
                ViewBag.MensajeExito = "¡Ubicación guardada y encontrada en el mapa!";

                // Volvemos a mostrar la MISMA vista, pero ahora el modelo tiene las coordenadas reales
                return View(sucursal);
            }

            ModelState.AddModelError("", "No se encontró la dirección. Intenta añadir la ciudad o país.");

            // Si hay error, le volvemos a poner las coordenadas por defecto para que no explote el mapa
            sucursal.Latitud = 40.4168;
            sucursal.Longitud = -3.7038;
            return View(sucursal);
        }

        // API: Geocodificación inversa (de coordenadas a dirección)
        [HttpGet]
        public async Task<IActionResult> GetAddressFromCoordinates(double lat, double lng)
        {
            try
            {
                var nominatimWebInterface = new NominatimWebInterface(_httpClientFactory);
                var geocoder = new ReverseGeocoder(nominatimWebInterface, null);
                
                var request = new ReverseGeocodeRequest
                {
                    Latitude = lat,
                    Longitude = lng,
                    BreakdownAddressElements = true,
                    ShowExtraTags = false,
                    ShowAlternativeNames = false,
                    ShowGeoJSON = false
                };

                var response = await geocoder.ReverseGeocode(request);

                if (response != null && !string.IsNullOrEmpty(response.DisplayName))
                {
                    return Json(new
                    {
                        success = true,
                        address = response.DisplayName,
                        latitude = lat,
                        longitude = lng
                    });
                }

                return Json(new
                {
                    success = false,
                    message = "No se pudo obtener la dirección para estas coordenadas"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Error: {ex.Message}"
                });
            }
        }
    }
}