using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace taskManagerApi.Models
{
    [Table("tb_task")]
    public class TodoTask
    {

        public TodoTask() { }

        public TodoTask(string description, int userId)
        {
            Description = description;
            UserId = userId;
            Status = TaskStatus.Pending;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status é obrigatório")]
        public TaskStatus Status { get; set; }

        public DateTime CreateAt { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; } = null;

    }
}