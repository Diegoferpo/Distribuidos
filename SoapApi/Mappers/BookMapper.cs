using SoapApi.Dtos;
using SoapApi.Infrastructure.Entities;
using SoapApi.Models;

namespace SoapApi.Mappers
{
    public static class BookMapper
    {
        public static BookModel ToModel(this BookEntity book)
        {
            if (book is null){
                return null;
            }
            return new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                PublishedDate = book.PublishedDate
            };
        }
        public static BookNameRequestDto ToDto(this BookModel book)
        {
            return new BookNameRequestDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                PublishedDate = book.PublishedDate
            };
        }
    }
}