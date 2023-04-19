using Sales.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sales.Shared.Mappers.States
{
    public class UpdateStateDto
    {
        [Display(Name = "Estado/Departamento/Provincia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }

        public static implicit operator UpdateStateDto (State state)
        {
            return new UpdateStateDto() { Name = state.Name, CountryId = state.CountryId };
        }
    }
}
