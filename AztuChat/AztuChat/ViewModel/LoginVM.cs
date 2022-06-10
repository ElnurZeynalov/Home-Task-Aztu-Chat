using System.ComponentModel.DataAnnotations;

namespace AztuChat.ViewModel
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required]
        public string Password { get; set; }
    }
}
