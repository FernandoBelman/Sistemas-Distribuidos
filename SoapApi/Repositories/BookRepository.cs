using System.ServiceModel;
using Microsoft.EntityFrameworkCore;
using SoapApi.Dtos;
using SoapApi.Infrastructure;
using SoapApi.Infrastructure.Entities;
using SoapApi.Mappers;

namespace SoapApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RelationalDbContext _dbContext;

        public BookRepository(RelationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            return book.ToModel(); 
        }
    }
}
