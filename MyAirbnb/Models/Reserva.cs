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
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataCheckin { get; set; }

        [Required]
        [DisplayName("Data do Check-out")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataCheckout { get; set; }

        [DefaultValue(false)]
        public bool Confirmado { get; set; }

        [DefaultValue(false)]
        public bool Prepared { get; set; }

        [DefaultValue(false)]
        public bool Delivered { get; set; }

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
