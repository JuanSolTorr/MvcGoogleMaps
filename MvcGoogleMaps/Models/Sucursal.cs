using System.ComponentModel.DataAnnotations;

namespace MvcGoogleMaps.Models
{
    public class Sucursal
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Dirección Completa")]
        public required string DireccionTexto { get; set; }

        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
