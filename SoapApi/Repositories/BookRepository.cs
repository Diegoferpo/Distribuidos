using System.CodeDom;
using System.ServiceModel;
using Microsoft.EntityFrameworkCore;
using SoapApi.Contracts;
using SoapApi.Dtos;
using SoapApi.Infrastructure;
using SoapApi.Mappers;
using SoapApi.Models;


namespace SoapApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RelationalDbContext _dbContext;

        public BookRepository (RelationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList <BookModel>> GetByNameAsync(string title, CancellationToken cancellationToken){
            var book = await _dbContext.Books.AsNoTracking().Where(s => s.Title.Contains(title)).ToListAsync(cancellationToken);
            return book.Select(book => book.ToModel()).ToList();
        }
    }
}