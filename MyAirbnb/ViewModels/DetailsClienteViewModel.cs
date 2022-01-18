using MyAirbnb.Models;
using System.Collections.Generic;

namespace MyAirbnb.ViewModels
{
    public class DetailsClienteViewModel
    {
        public ApplicationUser cliente { get; set; }

        public List<Classificacao> classificacoes { get; set; }
    }
}
