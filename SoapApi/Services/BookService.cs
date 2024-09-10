using System.Data;
using System.ServiceModel;
using SoapApi.Contracts;
using SoapApi.Dtos;
using SoapApi.Mappers;
using SoapApi.Repositories;

namespace SoapApi.Services
{
    public class BookService : IBookContract {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository){
            _bookRepository = bookRepository;
        }

        public async Task <IList<BookNameRequestDto>> GetBookByName(string title, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByNameAsync(title, cancellationToken);
            

            if(book.Any()){
            return book.Select(book => book.ToDto()).ToList();
            }

            throw new FaultException(reason: "No hay ningun libro con " + title + " como titulo");
        }
    }
}