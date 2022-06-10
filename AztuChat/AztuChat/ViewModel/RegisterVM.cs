using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AztuChat.ViewModel
{
    public class RegisterVM
    {
        [Required,MaxLength(40)]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password),Required]
        public string Password { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
