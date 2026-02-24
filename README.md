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
