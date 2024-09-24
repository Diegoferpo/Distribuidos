using RespApi.Repositories;
using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;

public class GroupService : IGroupService {

    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private IUserRepository? userRepository;

    public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }
    public async Task<GroupUserModel> GetGroupByIdAsync (string id, CancellationToken cancellationToken){

        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);
        if (group is null){
            return null;
        }

        return new GroupUserModel{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(
                group.Users.Select(userId => _userRepository.GetByIdAsync(
                    userId, cancellationToken)))).Where(user => user != null)
                    .ToList()
        };
    }



    public async Task<IList<GroupUserModel>> GetByNameAsync (string name, int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken){
        var group = await _groupRepository.GetByNameAsync(name, pageNumber, pageSize, orderBy, cancellationToken);
        if (group is null){
            return new List<GroupUserModel>();
        }
        var groupUserModels = await Task.WhenAll(group.Select(async group => new GroupUserModel{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(group.Users.Select(async user => await _userRepository.GetByIdAsync(user, cancellationToken)))).ToList()
        }));

        return groupUserModels.ToList();
    }
}
