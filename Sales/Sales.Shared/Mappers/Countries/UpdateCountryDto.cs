using Sales.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sales.Shared.Mappers.Countries
{
    public class UpdateCountryDto
    {
        [Display(Name = "País")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Name { get; set; } = string.Empty;

        public static implicit operator UpdateCountryDto(Country country)
        {
            return new UpdateCountryDto { Name = country.Name };
        }
    }
}
