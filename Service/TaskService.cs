using Microsoft.EntityFrameworkCore;
using taskManagerApi.Data;
using taskManagerApi.Dtos;
using taskManagerApi.Models;

namespace taskManagerApi.Service
{
    public class TaskService
    {
        private readonly AppDbContext _db;

        public TaskService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TodoTask>> GetAllByUserAsync(int userId)
        {
            return await _db.TodoTasks
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreateAt)
                .ToListAsync();
        }
        public async Task<TodoTask> CreateAsync(CreateTaskDto dto, int userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Description))
            {
                throw new ArgumentException("Descrição obrigatória");
            }

            var task = new TodoTask
            {
                Description = dto.Description,
                Status = Models.TaskStatus.Pending,
                UserId = userId,
                CreateAt = DateTime.UtcNow
            };

            _db.TodoTasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<TodoTask?> GetByIdAsync(int id, int userId)
        {
            return await _db.TodoTasks
                    .FirstOrDefaultAsync(task => task.Code == id && task.UserId == userId);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto, int userId)
        {
            var task = await _db.TodoTasks
                .FirstOrDefaultAsync(task => task.Code == id && task.UserId == userId);

            if (task == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                task.Description = dto.Description;

            if (task.Status == Models.TaskStatus.Pending && dto.Status == Models.TaskStatus.Completed)
                task.Status = Models.TaskStatus.Completed;
            else if (dto.Status != Models.TaskStatus.Pending)
                return false;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var task = await _db.TodoTasks
                .FirstOrDefaultAsync(t => t.Code == id && t.UserId == userId);

            if (task == null) return false;

            _db.TodoTasks.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }
    }


}