using System.ServiceModel;
using SoapApi.Contracts;
using SoapApi.Dtos;
using SoapApi.Repositories;

namespace SoapApi.Services
{
    public class BookService : IBookContract
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookModel> GetBookById(Guid id, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            if (book == null)
            {
                throw new FaultException("BookNotFound");
            }
            return book;
        }
    }
}
