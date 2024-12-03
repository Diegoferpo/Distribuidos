using RespApi.Models;

namespace RestApi.Repositories;

public interface IGroupRepository{
    Task <GroupModel> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IList<GroupModel>> GetAllByNameAsync(string name, int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken);
    Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
    Task <GroupModel> CreateAsync (string name, Guid[] users, CancellationToken cancellationToken);
    Task<GroupModel> GetByNameAsync(string name, CancellationToken cancellationToken);
}