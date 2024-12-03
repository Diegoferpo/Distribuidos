namespace RespApi.Dtos;

public class CreateGroupRequest{
    public string Name { get; set; }
    public Guid[] Users { get; set; }
}