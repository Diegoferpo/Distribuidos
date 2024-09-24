using RespApi.Models;
using RestApi.Models;

namespace RestApi.Repositories;

public interface IGroupRepository{
    Task <GroupModel> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IList<GroupModel>> GetByNameAsync(string name, int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken);
}