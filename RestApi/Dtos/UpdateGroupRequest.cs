
namespace RestApi.Dtos
{
    public class UpdateGroupRequest
    {
        public string Name { get; set; }
        public Guid[] Users { get; set; }
    }
}