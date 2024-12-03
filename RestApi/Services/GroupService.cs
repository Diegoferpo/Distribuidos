
using System.Text.RegularExpressions;
using RespApi.Repositories
using RestApi.Exceptions;
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



    public async Task<IList<GroupUserModel>> GetAllByNameAsync (string name, int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken){
        var group = await _groupRepository.GetAllByNameAsync(name, pageNumber, pageSize, orderBy, cancellationToken);
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

    public async Task DeleteGroupByIdAsync(string id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);

        if (group is null)
        {
            throw new GroupNotFoundException();
        }

        await _groupRepository.DeleteByIdAsync(id, cancellationToken);
    }


    public async Task<GroupUserModel> CreateGroupAsync(string name, Guid[] users, CancellationToken cancellationToken)
    {
        if (users.Length == 0){
            throw new InvalidGroupRequestFormatException();
        }

        var groups = await _groupRepository.GetByNameAsync(name, cancellationToken);
        if (groups is null){
            throw new GroupAlreadyExistsException();
        }

        foreach(var userId in users){
            var user = _userRepository.GetByIdAsync(userId, cancellationToken);
            if(user == null){
                throw new UserAlreadyExistsException();
            }
        }
        var group = await _groupRepository.CreateAsync(name, users, cancellationToken);
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

    public async Task UpdateGroupAsync(string id, string name, Guid[] users, CancellationToken cancellationToken)
    {
        if (users.Length == 0){
            throw new InvalidGroupRequestFormatException();
        }

        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);
        if (group is null){
            throw new GroupNotFoundException();
        }

        var groups  = await _groupRepository.GetByNameAsync(name, cancellationToken);
        if (groups is not null && groups.Id != id){
            throw new GroupAlreadyExistsException();
        }

        foreach(var userId in users){
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if(user == null){
                throw new UserAlreadyExistsException();
            }
        }

        await _groupRepository.UpdateGroupAsync(id, name, users, cancellationToken);
    }

}
