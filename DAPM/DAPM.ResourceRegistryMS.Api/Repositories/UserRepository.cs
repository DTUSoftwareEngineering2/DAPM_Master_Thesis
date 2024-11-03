using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAPM.ResourceRegistryMS.Api.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ILogger<IUserRepository> _logger;
        private readonly ResourceRegistryDbContext _context;
        public UserRepository(ILogger<IUserRepository> logger, ResourceRegistryDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<User> AddUser(User user)
        {

            if (_context.Users.Any(p => p.Id == user.Id))
            {
                return user;
            }

            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<User> GetUserByMail(String mail)
        {
            return await _context.Users
                .Where(r => r.Mail == mail)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            // await _context.Users
            //    .Where(r => r.Id == id)
            //   .FirstOrDefaultAsync();

            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> UpdateAcceptStatus(Guid id, int newStatus)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            user.accepted = newStatus;
            await _context.SaveChangesAsync();

            return user;

        }


    }
}
