using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAirbnb.Models
{
    public class Checklist
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required, DisplayName("Momento de Preparação")]
        public bool MomentoPreparacao { get; set; }

        [Required, DisplayName("Momento de Entrega")]
        public bool MomentoEntrega { get; set; }

        //TODO: alterar isto
        [Required, ForeignKey("Dono")]
        public string DonoId { get; set; }

        public ApplicationUser Dono { get; set; }

        [Required]
        [ForeignKey("Categoria"), DisplayName("Categoria")]
        public int CategoriaId { get; set; }
        
        public virtual Categoria Categoria { get; set; }


    }
}
