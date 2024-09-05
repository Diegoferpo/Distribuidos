using System.CodeDom;
using Microsoft.EntityFrameworkCore;
using SoapApi.Contracts;
using SoapApi.Dtos;
using SoapApi.Infrastructure;
using SoapApi.Mappers;
using SoapApi.Models;

namespace SoapApi.Repositories{



    public class UserRepository : IUserRepository
    {
        private readonly RelationalDbContext _dbContext;

        public UserRepository (RelationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task <UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            return user.ToModel();
        }

        public async Task <IList<UserModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.AsNoTracking().ToListAsync(cancellationToken);
            return users.Select(users => users.ToModel()).ToList();
        }

        public async Task <IList<UserModel>> GetAllByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.AsNoTracking().Where(s => s.Email.Contains(email)).ToListAsync(cancellationToken);
            return users.Select(users => users.ToModel()).ToList();
        }

        public Task<IList<UserModel>> GetAllByEmail(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync (UserModel user, CancellationToken cancellationToken)
        {
            var userEntity = user.ToEntity();

            _dbContext.Users.Remove(userEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task <UserModel> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var userEntity = user.ToEntity();
            userEntity.Id = Guid.NewGuid();
            await _dbContext.AddAsync(userEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userEntity.ToModel();
        }

        
    }
}