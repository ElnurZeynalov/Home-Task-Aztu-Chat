using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace AztuChat.Models
{
    public class UserImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
