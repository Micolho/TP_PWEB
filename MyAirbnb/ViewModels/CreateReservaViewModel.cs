using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyAirbnb.ViewModels
{
    public class CreateReservaViewModel
    {

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

        public int ImovelId { get; set; }
        public string ImovelNome { get; set; }
    }
}
