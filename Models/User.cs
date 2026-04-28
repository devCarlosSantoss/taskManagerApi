using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace taskManagerApi.Models
{
    [Table("tb_user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório")]

        public string Username { get; private set; } = string.Empty;

        [Required(ErrorMessage = "A senha e obrigatória")]
        public string Password { get; private set; } = string.Empty;

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        public string FullName { get; private set; } = string.Empty;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public ICollection<TodoTask> Tasks { get; private set; } = new HashSet<TodoTask>();

        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        public User(string username, string password, string fullName)
        {
            Username = username;
            SetPassword(password);  // 👈 CHAME ISSO!
            FullName = fullName;
            RegistrationDate = DateTime.UtcNow;
        }

    }
}