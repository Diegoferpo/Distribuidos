namespace RespApi.Models;

public class GroupModel{
    public string Id { get; set; }
    public string Name { get; set; }
    public Guid[] Users { get; set; }
    public DateTime CreationDate { get; set;}
}