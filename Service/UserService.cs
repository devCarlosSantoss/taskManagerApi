
using Microsoft.EntityFrameworkCore;
using taskManagerApi.Data;
using taskManagerApi.Dtos;
using taskManagerApi.Models;

namespace taskManagerApi.Service
{
    public class UserService
    {
        private readonly AppDbContext _db;
        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task RegisterAsync(UserRegisterDto dto)
        {
            var user = new User(dto.Username, dto.Password, dto.FullName);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Username == username);

            if(user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }
    }
}