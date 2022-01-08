using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyAirbnb.Models
{
    public class Imovel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required, Range(0,9999)]
        public float EspacoM2 { get; set; }

        [Required, DisplayName("Preço por noite")]
        public float PrecoPorNoite { get; set; }

        [Required, Range(0, 10), DisplayName("Número de camas")]
        public int NumeroCamas { get; set; }

        [Required, DisplayName("Tem Cozinha")]
        public bool TemCozinha { get; set; }

        [Required, DisplayName("Tem Jacuzzi")]
        public bool TemJacuzzi { get; set; }

        [Required, DisplayName("Tem Piscina")]
        public bool TemPiscina { get; set; }

        [Required, DataType(DataType.Time), DisplayName("Check-in")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime HoraCheckIn { get; set; }

        [Required, DataType(DataType.Time), DisplayName("Check-out")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime HoraCheckOut { get; set; }


        [Required]
        public int TipoImovelID { get; set; }

        public virtual Categoria TipoImovel { get; set; }



        public string Descricao { get; set; }

        public string Image { get; set; }
    }
}
