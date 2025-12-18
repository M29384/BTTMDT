    
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [StringLength(40 , MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
       

    }
}
