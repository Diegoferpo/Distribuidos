using System.Runtime.Serialization;


namespace SoapApi.Dtos
{
    [DataContract]
    public class BookNameRequestDto
    {
        [DataMember]
        public Guid Id {get; set; }
        [DataMember]
        public String Title {get; set; } = null!;
        [DataMember]
        public String Author {get; set; } = null!;
        [DataMember]
        public String Publisher {get; set; } = null!;
        [DataMember]
        public DateTime PublishedDate {get; set; }
    }
}