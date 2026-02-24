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

Si descargas el proyecto, Visual Studio restaurará los paquetes automáticamente. No obstante, estas son las librerías clave utilizadas:

1.  **`Nominatim.API`**: Cliente para la geocodificación gratuita.
2.  **`Microsoft.EntityFrameworkCore.SqlServer`**: Conector para SQL Server.
3.  **`Microsoft.EntityFrameworkCore.Tools`**: Para gestionar las migraciones de base de datos.
4.  **Leaflet.js** (vía CDN): Para el renderizado del mapa en el navegador.

---

## 🛠️ Configuración en un nuevo equipo

Para ejecutar este proyecto en otro ordenador (por ejemplo, para una presentación), sigue estos pasos:

### 1. Clonar el repositorio
```bash
git clone [https://github.com/JuanSolTorr/MvcGoogleMaps.git](https://github.com/JuanSolTorr/MvcGoogleMaps.git)
