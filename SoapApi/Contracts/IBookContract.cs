using System.ServiceModel;
using SoapApi.Dtos;

namespace SoapApi.Contracts
{
    [ServiceContract]
    public interface IBookContract
    {
        [OperationContract]
        public Task <IList<BookNameRequestDto>> GetBookByName(string title, CancellationToken cancellationToken);
    }
}