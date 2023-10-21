using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class Proveedores
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "El campo Nombre del Proveedor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo Nombre del Proveedor debe tener como máximo 100 caracteres.")]
        [Display(Name = "Nombre del Proveedor")]
        public string NombreProveedor { get; set; }

        [Required(ErrorMessage = "El campo Ubicación de la Empresa es obligatorio.")]
        [StringLength(300, ErrorMessage = "El campo Ubicación de la Empresa debe tener como máximo 300 caracteres.")]
        [Display(Name = "Ubicación de la Empresa")]
        public string UbicacionEmpresa { get; set; }

        [Required(ErrorMessage = "El campo Correo Electrónico de la Empresa es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Correo Electrónico de la Empresa debe tener como máximo 50 caracteres.")]
        [EmailAddress(ErrorMessage = "El campo Correo Electrónico de la Empresa debe ser una dirección de correo electrónico válida.")]
        [Display(Name = "Correo Electrónico de la Empresa")]
        public string CorreoElectronicoEmpresa { get; set; }

        [Required(ErrorMessage = "El campo Número Telefónico de la Empresa es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo Número Telefónico de la Empresa debe tener como máximo 25 caracteres.")]
        [Display(Name = "Número Telefónico de la Empresa")]
        public string NumeroTelefonicoEmpresa { get; set; }
    }
}
