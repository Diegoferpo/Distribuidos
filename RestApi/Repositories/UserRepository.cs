using RestApi.Repositories;
using RestApi.Models;
using RestApi.Infrasctructure.Soap.SoapContracts;
using System.ServiceModel;
using RestApi.Mappers;

namespace RespApi.Repositories;

public class UserRepository : IUserRepository{

    private readonly ILogger<UserRepository> _logger;
    

    private readonly IUserContract _userContract;

    public UserRepository(ILogger<UserRepository> logger, IConfiguration configuration){
        _logger = logger;
        var binding = new BasicHttpBinding();
        var endpoint = new EndpointAddress(configuration.GetValue<string>("UserServiceEndpoint"));
        _userContract = new ChannelFactory<IUserContract>(binding, endpoint).CreateChannel();

    }

    public async Task<UserModel> GetByIdAsync(Guid userId, CancellationToken cancellationToken){

        try{
            var user = await _userContract.GetUserById(userId, cancellationToken);
            return user.ToDomain();

        } catch(FaultException ex) when (ex.Message == "user not found"){
            _logger.LogError($"User with Id: {userId} not found.");
            return null;
        }

    }
}