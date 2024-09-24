using RestApi.Models;

namespace RestApi.Services;

public interface IGroupService{

    Task <GroupUserModel> GetGroupByIdAsync(string id, CancellationToken cancellationToken);

    Task<IList<GroupUserModel>> GetByNameAsync(string name, int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken);
}