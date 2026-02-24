using Microsoft.EntityFrameworkCore;
using MvcGoogleMaps.Models;
using System.Collections.Generic;

namespace MvcGoogleMaps.Data
{
    public class GoogleMapsContext : DbContext
    {
        public GoogleMapsContext(DbContextOptions<GoogleMapsContext> options) : base(options)
        {
        }

        public DbSet<Sucursal> Sucursales => Set<Sucursal>();
    }
}
