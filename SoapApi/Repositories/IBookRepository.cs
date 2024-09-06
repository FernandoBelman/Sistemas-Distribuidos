using SoapApi.Dtos;

namespace SoapApi.Repositories
{
    public interface IBookRepository
    {
        Task<BookModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
