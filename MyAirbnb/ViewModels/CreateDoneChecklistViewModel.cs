using MyAirbnb.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAirbnb.ViewModels
{
    public class CreateDoneChecklistViewModel
    {
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

        public bool IsPreparacao { get; set; }

        //[ForeignKey("Responsavel")]
        //public string ResponsavelId { get; set; }

        //public ApplicationUser Responsavel { get; set; }
    }
}
