namespace SoapApi.Infrastructure.Entities;

public class UserEntity
{
    public Guid Id {get; set; }
    public String Email {get; set; } = null!;
    public String FirstName {get; set; } = null!;
    public String LastName {get; set; } = null!;
    public DateTime Birthday {get; set; }
}