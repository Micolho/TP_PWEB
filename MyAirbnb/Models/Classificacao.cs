using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }

        public virtual Reserva Reserva { get; set; }


        //TODO: alterar isto
        //public string NomeDoUtilizador { get; set; }

        [ForeignKey("Utilizador")]
        public string UtilizadorId { get; set; }

        public ApplicationUser Utilizador { get; set; }
    }
}