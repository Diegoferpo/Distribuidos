using SoapApi.Models;

namespace SoapApi.Repositories
{
    public interface IBookRepository
    {
        public Task <IList<BookModel>> GetByNameAsync(string title, CancellationToken cancellationToken);
    }
}