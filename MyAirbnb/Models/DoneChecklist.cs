using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAirbnb.Models
{
    public class DoneChecklist
    {

        public int Id { get; set; }

        [DisplayName("Observações")]
        public string Observacoes { get; set; }

        //Lista imagens
        public ICollection<Imagens> Imagens { get; set; }
        
        [ForeignKey("ChecklistId")]
        public int? ChecklistId { get; set; }
        public Checklist Checklist { get; set; }

        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }

        //TODO: ALTERAR ISTO
        [ForeignKey("Responsavel")]
        public string ResponsavelId { get; set; }

        public ApplicationUser Responsavel { get; set; }

    }
}
