using System.ComponentModel.DataAnnotations;

namespace MyAirbnb.ViewModels
{
    public class CreateEmpresaViewModel
    {
        [Required]
        public string Nome { get; set; }

    }
}
