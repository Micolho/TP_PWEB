using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAirbnb.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [ForeignKey("Dono")]
        public string DonoId { get; set; }

        public ApplicationUser Dono { get; set; }

        [NotMapped]
        public ICollection<ApplicationUser> Funcionarios { get; set; }
    }
}
