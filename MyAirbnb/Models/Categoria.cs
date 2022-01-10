using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyAirbnb.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }

}
