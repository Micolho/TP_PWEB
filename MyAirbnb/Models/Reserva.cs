using System;
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
        [DisplayName("Data do primeiro dia")]
        [DataType(DataType.Date)]
        public DateTime DataDeInicio { get; set; }

        [Required]
        [DisplayName("Data do último dia")]
        [DataType(DataType.Date)]
        public DateTime DataDeFim { get; set; }

        //Imovel
        [Required]
        public int ImovelID { get; set; }

        [ForeignKey("ImovelID")]
        public virtual Imovel Imovel { get; set; }
    }
}
