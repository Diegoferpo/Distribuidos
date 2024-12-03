using System.Net;
using Microsoft.AspNetCore.Mvc;
using RespApi.Dtos;
using RestApi.Exceptions;
using RestApi.Mappers;
using RestApi.Services;

namespace RestApi.Controller;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    // localhost:port/groups/192282892929
    [HttpGet("{id}")]
    public async Task<ActionResult<GroupResponse>> GetGroupById(string id, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByIdAsync(id, cancellationToken);
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

        var lista = await _groupService.GetAllByNameAsync(name, pageNumber, pageSize, orderBy, cancellationToken);
        
        return Ok(lista.Select(lista => lista.ToDto()).ToList());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGroup(string id, CancellationToken cancellationToken)
    {
        try {
            await _groupService.DeleteGroupByIdAsync(id, cancellationToken);
            return NoContent();

        } catch(GroupNotFoundException) {
            return NotFound();
        }
        
        
    }

    [HttpPost]
    public async Task<ActionResult<GroupResponse>> CreateGroupRequest([FromBody] CreateGroupRequest groupRequest, CancellationToken cancellationToken){
        try {
            var group = await _groupService.CreateGroupAsync(groupRequest.Name, groupRequest.Users, cancellationToken);
            return CreatedAtAction(nameof(GetGroupById), new { id = group.Id }, group.ToDto());

        } catch(InvalidGroupRequestFormatException){
            return BadRequest(NewValidationProblemDetails("One or more validation erros occurred", HttpStatusCode.BadRequest, new Dictionary<string, string[]>{
                {"Groups", ["Users array is empty"]}
            }));
        }catch (GroupAlreadyExistsException){
            return Conflict(NewValidationProblemDetails("One or more validation erros occurred", HttpStatusCode.Conflict, new Dictionary<string, string[]>{
                {"Groups", ["Gruop with same name already exists"]}
            }));

        }catch(UserAlreadyExistsException){
            return BadRequest(NewValidationProblemDetails("One or more validation problems ocurred", HttpStatusCode.BadRequest, new Dictionary<string, string[]> {
                {"Id", ["The specified IdUser already exists in the database"]}
            }));

        }
    }

    private static ValidationProblemDetails NewValidationProblemDetails(string title, HttpStatusCode statusCode, Dictionary<string, string[]> errors){
        return new ValidationProblemDetails{
            Title = title,
            Status = (int) statusCode,
            Errors = errors
        };
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GroupResponse>> UpdateGroup(string id, [FromBody] UpdateGroupRequest groupRequest, CancellationToken cancellationToken){
        try {
            await _groupService.UpdateGroupAsync(id, groupRequest.Name, groupRequest.Users, cancellationToken);
            return NoContent();

            } catch(InvalidGroupRequestFormatException){
            return BadRequest(NewValidationProblemDetails("One or more validation erros occurred", HttpStatusCode.BadRequest, new Dictionary<string, string[]>{
                {"Groups", ["Users array is empty"]}

            }));

        }catch (GroupAlreadyExistsException){
            return Conflict(NewValidationProblemDetails("One or more validation erros occurred", HttpStatusCode.Conflict, new Dictionary<string, string[]>{
                {"Groups", ["Gruop with same name already exists"]}

            }));
    
        } catch(UserAlreadyExistsException){
            return BadRequest(NewValidationProblemDetails("One or more validation problems ocurred", HttpStatusCode.BadRequest, new Dictionary<string, string[]> {
                {"Id", ["The specified IdUser already exists in the database"]}
            }));
        }
    }

}

