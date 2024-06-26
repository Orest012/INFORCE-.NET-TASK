using System.ComponentModel.DataAnnotations;

namespace INFORCE_.NET_TASK.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
