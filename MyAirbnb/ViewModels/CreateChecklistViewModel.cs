using System.ComponentModel.DataAnnotations;

namespace MyAirbnb.ViewModels
{
    public class CreateChecklistViewModel
    {

        [Required]
        [Display(Name ="Descricao")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        
        [Display(Name = "Momento de Entrega")]
        public bool MomentoEntrega { get; set; }
        
        [Display(Name = "Momento de Preparacao")]
        public bool MomentoPreparacao { get; set; }

    }
}
