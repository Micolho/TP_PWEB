using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyAirbnb.Models
{
    public class Classificacao
    {
        [Key]
        public int Id { get; set; }

        [Range(0,5)]
        public int Estrelas { get; set; }

        [DisplayName("Comentário")]
        public string Comentario { get; set; }
    }
}
