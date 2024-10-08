using System.ServiceModel;
using SoapApi.Contracts;
using SoapApi.Dtos;
using SoapApi.Mappers;
using SoapApi.Repositories;

namespace SoapApi.Services;

public class UserService : IUserContract{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }


    public async Task<UserResponseDto> CreateUser(UserCreateRequestDto userRequest, CancellationToken cancellationToken)
    {
        var user = userRequest.ToModel();
        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
        return createdUser.ToDto();
    }

    public async Task<bool> DeleteUserById(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null){
            throw new FaultException("User not found");
        }

        await _userRepository.DeleteByIdAsync(user, cancellationToken);
        return true;
    }

    public async Task<IList<UserResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return users.Select(user => user.ToDto()).ToList();
    }

    public async Task<IList<UserResponseDto>> GetAllByEmail(string email, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllByEmailAsync(email, cancellationToken);
        if (users is not null){
            return users.Select(user => user.ToDto()).ToList();
        }
        throw new FaultException("Ningun usuario con ese correo");
        
    }

    public async Task<UserResponseDto> GetUserById(Guid userId, CancellationToken cancellationToken){
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if(user is not null){
            return user.ToDto();
        }

        throw new FaultException("User not found");
    }


    public async Task<UserResponseDto> UpdateUser(Guid userId, UserUpdateRequestDto updateRequest, CancellationToken cancellationToken)
    {
        //aqui obtenemos el usuario mediante su id 
        var existingUser = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (existingUser is null)
        {
            throw new FaultException("User not found. ");
        }

        //actualizamos los campos con los datos del dto
        existingUser.FirstName = updateRequest.FirstName;
        existingUser.LastName = updateRequest.LastName;
        existingUser.BirthDate = updateRequest.Birthday;

        var updateUser = await _userRepository.UpdateAsync(existingUser, cancellationToken);

        return updateUser.ToDto();
    }

}

