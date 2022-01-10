using System.ComponentModel.DataAnnotations;

namespace MyAirbnb.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name ="Role")]
        public string RoleName { get; set; }
    }
}
