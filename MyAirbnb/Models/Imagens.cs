using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAirbnb.Models
{
    public class Imagens
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

/*        [ForeignKey("DoneChecklistId"),]
        public int? DoneChecklistId { get; set; }

        
        public virtual DoneChecklist DoneChecklist { get; set; }

        [ForeignKey("ImovelId")]
        public int? ImovelId { get; set; }

        
        public virtual Imovel Imovel { get; set; }*/

    }
}
