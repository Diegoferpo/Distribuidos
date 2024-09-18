using System.ServiceModel;


namespace RestApi.Infrasctructure.Soap.SoapContracts;

[ServiceContract]
public interface IUserContract
{

    [OperationContract]
    public Task<UserResponseDto> GetUserById(Guid userId, CancellationToken cancellationToken);

    [OperationContract]
    public Task <IList<UserResponseDto>> GetAll(CancellationToken cancellationToken);

    [OperationContract]
    public Task<IList<UserResponseDto>> GetAllByEmail(string email, CancellationToken cancellationToken);

    [OperationContract]
    public Task<bool> DeleteUserById (Guid userId, CancellationToken cancellationToken);

}

