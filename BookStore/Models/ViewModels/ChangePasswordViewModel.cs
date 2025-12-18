using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class ChangePasswordViewModel    
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6)]
        public string? OldPassword { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? ConfirmNewPassword { get; set; }

    }
}
