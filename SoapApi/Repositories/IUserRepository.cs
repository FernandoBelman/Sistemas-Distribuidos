using SoapApi.Dtos;

namespace SoapApi.Repositories;

public interface IUserRepository{
    public Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IList<UserModel>> GetAllAsync(CancellationToken cancellationToken);
    public Task<IList<UserModel>> GetAllByEmailAsync(string email, CancellationToken cancellationToken);
    public Task AddUserAsync(UserModel user, CancellationToken cancellationToken);  // Método para agregar un usuario
}
