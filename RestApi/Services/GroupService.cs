using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupReposotory;

    public GroupService(IGroupRepository groupRepository){
        _groupReposotory = groupRepository;
    }



    public async Task<GroupUserModel> GetGroupByIdAsync(string Id, CancellationToken cancellationToken)
    {
        var group = await _groupReposotory.GetByIdAsync(Id, cancellationToken);
        if(group is null){
            return null;
        }
        return new GroupUserModel{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate
        };
    }
}