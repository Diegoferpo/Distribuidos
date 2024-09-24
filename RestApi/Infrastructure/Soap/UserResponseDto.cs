using System.Runtime.Serialization;

namespace RestApi.Infrasctructure.Soap.SoapContracts;

[DataContract (Namespace = "http://schemas.datacontract.org/2004/07/SoapApi.Dtos")]

public class UserResponseDto
{

        [DataMember(Order = 1)]
        public Guid Id {get; set; }
        [DataMember (Order = 2)]
        public String Email {get; set; } = null!;
        [DataMember (Order = 3)]
        public String FirstName {get; set; } = null!;
        [DataMember (Order = 4)]
        public String LastName {get; set; } = null!;
        [DataMember (Order = 5)]
        public DateTime BirthDate {get; set; }
    
}

