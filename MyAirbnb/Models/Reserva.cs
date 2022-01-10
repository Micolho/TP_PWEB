using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAirbnb.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Data do Check-in")]
        [DataType(DataType.Date)]
        public DateTime DataCheckin { get; set; }

        [Required]
        [DisplayName("Data do Check-out")]
        [DataType(DataType.Date)]
        public DateTime DataCheckout { get; set; }

        [DefaultValue(false)]
        public bool Confirmado { get; set; }

        //Imovel
        [ForeignKey("ImovelId")]
        public int ImovelId { get; set; }
        
        public virtual Imovel Imovel { get; set; }

        public ICollection<DoneChecklist> DoneChecklist { get; set; }

        //TODO: alterar isto
        [ForeignKey("Cliente")]
        public string ClienteId { get; set; }

        public ApplicationUser Cliente { get; set; }
    }
}
