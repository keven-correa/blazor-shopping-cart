using Sales.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Mappers.States
{
    public class CreateStateDto
    {
        [Display(Name = "Estado/Departamento/Provincia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }

        public static implicit operator CreateStateDto (State state)
        {
            return new CreateStateDto() { Name = state.Name, CountryId = state.CountryId };
        }
    }
}
