using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using RestApi.Mappers;
using RestApi.Services;

namespace RestApi.Controller;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase {

    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }
    //localhosts:port/groups/192282892929
    [HttpGet("{id}")]
    public async Task <ActionResult<GroupResponse>> GetGroupById(string id, CancellationToken cancellationToken){
        var group = await _groupService.GetGroupByIdAsync(id, cancellationToken);
        if (group is null){
            return NotFound();
        }

        return Ok(group.ToDto());
    }
}