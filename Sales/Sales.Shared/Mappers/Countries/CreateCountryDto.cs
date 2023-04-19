using Sales.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Mappers.Countries
{
    public class CreateCountryDto
    {
        [Display(Name = "País")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Name { get; set; } = string.Empty;

        public static implicit operator CreateCountryDto(Country country) {
            return new CreateCountryDto
            {
                Name = country.Name,
            };
        }
    }
}
