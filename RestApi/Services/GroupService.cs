using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;

public class GroupService : IGroupService {

    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    public async Task<GroupUserModel> GetGroupByIdAsync (string id, CancellationToken cancellationToken){

        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);
        if (group is null){
            return null;
        }

        return new GroupUserModel{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate
        };
    }

    public async Task<IList<GroupUserModel>> GetByNameAsync (string name, CancellationToken cancellationToken){
        var group = await _groupRepository.GetByNameAsync(name, cancellationToken);
        if (group is null){
            return new List<GroupUserModel>();
        }
        return group.Select(g => new GroupUserModel{
            Id = g.Id,
            Name = g.Name,
            CreationDate = g.CreationDate
        }).ToList();
    }
}