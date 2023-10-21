using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class Carros
    {
        [Key]
        public int IdCarro { get; set; }

        [Required(ErrorMessage = "El campo IdModelo es obligatorio.")]
        [Display(Name = "Modelo")]
        public int IdModelo { get; set; }

        [Required(ErrorMessage = "El campo Estilo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Estilo debe tener como máximo 50 caracteres.")]
        public string Estilo { get; set; }

        [Required(ErrorMessage = "El campo Año es obligatorio.")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El campo IdCombustible es obligatorio.")]
        [Display(Name = "Tipo de Combustible")]
        public int IdCombustible { get; set; }

        [Required(ErrorMessage = "El campo IdTransmision es obligatorio.")]
        [Display(Name = "Tipo de Transmisión")]
        public int IdTransmision { get; set; }

        [Required(ErrorMessage = "El campo Número de Puertas es obligatorio.")]
        [Range(2, 6, ErrorMessage = "El campo Número de Puertas debe estar entre 2 y 6.")]
        [Display(Name = "Número de Puertas")]
        public int NumeroPuertas { get; set; }

        [Required(ErrorMessage = "El campo Color Exterior es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Color Exterior debe tener como máximo 50 caracteres.")]
        [Display(Name = "Color Exterior")]
        public string ColorExterior { get; set; }

        [Required(ErrorMessage = "El campo Color Interior es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Color Interior debe tener como máximo 50 caracteres.")]
        [Display(Name = "Color Interior")]
        public string ColorInterior { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Ingreso es obligatorio.")]
        [Display(Name = "Fecha de Ingreso")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "El campo Placa es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo Placa debe tener como máximo 25 caracteres.")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "El campo IdFinanciamiento es obligatorio.")]
        [Display(Name = "Tipo de Financiamiento")]
        public int IdFinanciamiento { get; set; }

        [Required(ErrorMessage = "El campo Apartado es obligatorio.")]
        [StringLength(3, ErrorMessage = "El campo Apartado debe tener como máximo 3 caracteres.")]
        public string Apartado { get; set; }

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El campo Precio debe estar entre 0.01 y 1,000,000.00")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [ForeignKey("IdModelo")]
        public ModelosCarros? ModeloCarro { get; set; }

        [ForeignKey("IdCombustible")]
        public TipoCombustibles? Combustible { get; set; }

        [ForeignKey("IdTransmision")]
        public TipoTransmisiones? Transmision { get; set; }

        [ForeignKey("IdFinanciamiento")]
        public TipoFinanciamientos? Financiamiento { get; set; }
    }
}
