using System.ServiceModel;
using SoapApi.Dtos;

namespace SoapApi.Contracts
{
    [ServiceContract]
    public interface IBookContract
    {
        [OperationContract]
        Task<BookModel> GetBookById(Guid id, CancellationToken cancellationToken);
    }
}
