using MyAirbnb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAirbnb.ViewModels
{
    public class CreateImovelViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required, Range(0, 9999), DisplayName("Espaço m²")]
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

        [Required, Range(0, 10), DisplayName("Casa de banho")]
        public int numeroWC { get; set; }

        [Required, Range(0, 100), DisplayName("Número de Hóspedes")]
        public int NumeroPessoas { get; set; }

        [Required, DataType(DataType.Time), DisplayName("Hora de Check-in")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime HoraCheckIn { get; set; }

        [Required, DataType(DataType.Time), DisplayName("Hora de Check-out")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime HoraCheckOut { get; set; }

        [Required]
        public string Localidade { get; set; }

        [Required]
        public string Rua { get; set; }

        //Categoria
        [Required, DisplayName("Tipologia")]
        public int TipoImovelId { get; set; }

        public virtual Categoria TipoImovel { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public ICollection<Reserva> Reserva { get; set; }

        public ICollection<Imagens> Imagens { get; set; }

        [ForeignKey("Dono"), DisplayName("Dono")]
        public string DonoId { get; set; }

        public ApplicationUser Dono { get; set; }

        [ForeignKey("Responsavel"), DisplayName("Responsável")]
        public string ResponsavelId { get; set; }

        public ApplicationUser Responsavel { get; set; }

        [Required]
        public bool Listado { get; set; }

    }
}
