using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using RestApi.Mappers;
using RestApi.Services;

namespace RestApi.Controller;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    // localhost:port/groups/192282892929

    // localhost:port/groups/192282892929
    [HttpGet("{id}")]
    public async Task<ActionResult<GroupResponse>> GetGroupById(string id, CancellationToken cancellationToken)
    {
    public async Task<ActionResult<GroupResponse>> GetGroupById(string id, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByIdAsync(id, cancellationToken);
        if (group is null)
        {
        if (group is null)
        {
            return NotFound();
        }

        return Ok(group.ToDto());
    }

    // localhost:port/groups?name=GroupName
    [HttpGet]
    public async Task<ActionResult<IList<GroupResponse>>> GetGroupByName([FromQuery] string name, [FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string orderBy, CancellationToken cancellationToken)
    {

        var lista = await _groupService.GetByNameAsync(name, pageNumber, pageSize, orderBy, cancellationToken);
        
        return Ok(lista.Select(lista => lista.ToDto()).ToList());
    }
}

