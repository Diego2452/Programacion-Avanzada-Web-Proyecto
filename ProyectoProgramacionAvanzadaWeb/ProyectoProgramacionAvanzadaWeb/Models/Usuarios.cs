using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo IdTipoCedula es obligatorio.")]
        [Display(Name = "Tipo de Cédula")]
        public int IdTipoCedula { get; set; }

        [Required(ErrorMessage = "El campo IdRol es obligatorio.")]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El campo Cédula es obligatorio.")]
        [StringLength(20, ErrorMessage = "El campo Cédula debe tener como máximo 20 caracteres.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El campo Apellido Materno debe tener como máximo 50 caracteres.")]
        [Display(Name = "Apellido Materno")]
        public string Apellido_Materno { get; set; }

        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Apellido Paterno debe tener como máximo 50 caracteres.")]
        [Display(Name = "Apellido Paterno")]
        public string Apellido_Paterno { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [StringLength(255, ErrorMessage = "El campo Email debe tener como máximo 255 caracteres.")]
        [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección de correo electrónico válida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [StringLength(255, ErrorMessage = "El campo Contraseña debe tener como máximo 255 caracteres.")]
        public string Contrasenna { get; set; }

        [Required(ErrorMessage = "El campo IdSexo es obligatorio.")]
        [Display(Name = "Sexo")]
        public int IdSexo { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        [StringLength(20, ErrorMessage = "El campo Teléfono debe tener como máximo 20 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
        [StringLength(300, ErrorMessage = "El campo Dirección debe tener como máximo 300 caracteres.")]
        public string Direccion { get; set; }

        [ForeignKey("IdSexo")]
        public Sexo Sexo { get; set; }

        [ForeignKey("IdTipoCedula")]
        public TipoCedulas TipoCedula { get; set; }

        [ForeignKey("IdRol")]
        public Roles Rol { get; set; }
    }
}
