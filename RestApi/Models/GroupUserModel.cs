namespace RestApi.Models;

public class GroupUserModel{

    public string Id { get; set; }
    public string Name { get; set; }
    public IList<UserModel> Users { get; set; }
    public DateTime CreationDate { get; set; }
}