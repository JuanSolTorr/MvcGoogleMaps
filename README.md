# 🌍 MvcGoogleMaps - Geolocalización Gratuita con .NET 10

Este proyecto es una aplicación web moderna desarrollada en **ASP.NET Core MVC** que permite el registro de sucursales mediante una dirección de texto. La aplicación convierte automáticamente ese texto en coordenadas geográficas (Latitud y Longitud) y las visualiza en un mapa interactivo sin costo de APIs.

## 🚀 Características

- **Geocodificación Gratuita:** Uso de la API de **Nominatim (OpenStreetMap)** para traducir texto a coordenadas.
- **Mapas Interactivos:** Implementación de **Leaflet.js** para renderizar mapas dinámicos.
- **Arquitectura Profesional:** Uso de `IHttpClientFactory` y el patrón Inyección de Dependencias.
- **Persistencia de Datos:** Almacenamiento en **SQL Server** mediante **Entity Framework Core (Code-First)**.
- **Interfaz Unificada:** Formulario de búsqueda y visualización de mapa en una sola pantalla (SPA Feel).

---

## 📦 Requisitos y Dependencias

Estas son las librerías clave utilizadas en el proyecto:

1.  **`Nominatim.API`**: Cliente para la geocodificación gratuita.
2.  **`Microsoft.EntityFrameworkCore.SqlServer`**: Conector para SQL Server.
3.  **`Microsoft.EntityFrameworkCore.Tools`**: Para gestionar las migraciones de base de datos.
4.  **Leaflet.js** (vía CDN): Para el renderizado del mapa en el navegador.

---

## ⚙️ Código Clave de la Arquitectura

### 1. El Modelo (Sucursal.cs)
Uso de C# 10+ con miembros requeridos y anotaciones de datos.
```csharp
public class Sucursal
{
    [Key]
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string DireccionTexto { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
}
````
### 2. El Contexto de Datos (Data/GoogleMapsContext.cs)
```csharp
public class GoogleMapsContext(DbContextOptions<GoogleMapsContext> options) : DbContext(options)
{
    public DbSet<Sucursal> Sucursales => Set<Sucursal>();
}
```
### 3. El Controlador (Controllers/GoogleMapsController.cs)
Lógica de geocodificación asíncrona inyectando IHttpClientFactory.
```csharp
[HttpPost]
public async Task<IActionResult> Index(Sucursal sucursal)
{
    var geocoder = new ForwardGeocoder(new NominatimWebInterface(_httpClientFactory), null);
    var response = await geocoder.Geocode(new ForwardGeocodeRequest { queryString = sucursal.DireccionTexto });

    if (response != null && response.Length > 0)
    {
        sucursal.Latitud = response[0].Latitude;
        sucursal.Longitud = response[0].Longitude;
        _context.Sucursales.Add(sucursal);
        await _context.SaveChangesAsync();
    }
    return View(sucursal);
}
```
4. La Vista Unificada (Views/GoogleMaps/Index.cshtml)
Fragmento de la lógica de Leaflet para renderizar el mapa:
```csharp
var map = L.map('miMapa').setView([lat, lng], zoomLevel);
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);

if (sucursalId > 0) {
    L.marker([lat, lng]).addTo(map).bindPopup(nombre).openPopup();
}
```
## 🛠️ Configuración de la Base de Datos
Sigue estos pasos para replicar la base de datos en SQL Server Management Studio (SSMS):

1. Configurar Cadena de Conexión:
Abre appsettings.json y actualiza el servidor:
```csharp
"ConnectionStrings": {
  "DefaultConnection": "Server=NOMBRE_DE_TU_SERVIDOR;Database=MvcGoogleMapsDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
2. Ejecutar Comandos en Consola NuGet:
Abre la "Consola del Administrador de Paquetes" y ejecuta:
```csharp
Install-Package Microsoft.EntityFrameworkCore.Tools
Update-Database
```
## 📁 Estructura del Proyecto
```csharp
MvcGoogleMaps/
├── Controllers/
│   └── GoogleMapsController.cs   <-- Lógica y Geocodificación
├── Data/
│   └── GoogleMapsContext.cs      <-- Contexto de SQL Server
├── Migrations/                   <-- Historial de tablas de BD
├── Models/
│   └── Sucursal.cs               <-- Entidad de negocio
├── Views/
│   └── GoogleMaps/
│       └── Index.cshtml          <-- UI: Formulario + Mapa Leaflet
├── appsettings.json              <-- Configuración de conexión
└── Program.cs                    <-- Inyección de HttpClient y DB
```
