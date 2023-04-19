using Sales.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sales.Shared.Mappers.Cities
{
    public class UpdateCityDto
    {

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Name { get; set; } = string.Empty;
        public int StateId { get; set; }


        public static implicit operator UpdateCityDto(City city)
        {
            return new UpdateCityDto
            {
                Name = city.Name,
                StateId = city.StateId,
            };
        }
    }
}
