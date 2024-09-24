namespace RestApi.Dtos;

public class GroupResponse{
    public string Id { get; set; }
    public string Name { get; set; }

    public List<UserResponse> Users { get; set; }
    public DateTime CreationDate { get; set; }

}