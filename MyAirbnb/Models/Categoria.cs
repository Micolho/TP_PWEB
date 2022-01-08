﻿using System.ComponentModel.DataAnnotations;

namespace MyAirbnb.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }
    }

}
