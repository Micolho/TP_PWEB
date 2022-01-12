using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAirbnb.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{9,10}$")]
        public int NIF { get; set; }

        [Required, DisplayName("Cartão de Cidadão")]
        public string CC { get; set; }

        [Required, RegularExpression(@"^[0-9]{9}$"), DisplayName("Número de Telemóvel")]
        public int NumeroTelemovel { get; set; }

        [Required, DataType(DataType.Date), DisplayName("Data de Nascimento")]
        public DateTime DataDeNascimento { get; set; }

        public string Morada { get; set; }

        
        public int? EmpresaId { get; set; }
        
        public virtual Empresa Empresa { get; set; }


    }
}
