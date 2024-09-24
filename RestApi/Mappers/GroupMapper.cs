using RespApi.Models;
using RestApi.Dtos;
using RestApi.Infrasctructure.Mongo;
using RestApi.Models;

namespace RestApi.Mappers;

public static class GroupMapper{
    public static GroupResponse ToDto(this GroupUserModel group){
        return new GroupResponse{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate, 
            Users = group.Users.ToDto()
        };
    }

    public static List<UserResponse> ToDto(this IEnumerable<UserModel> users) {
        return users.Select(s => new UserResponse {
            Id = s.Id,
            Name = s.FirstName + " " + s.LastName,
            Email = s.Email
        }).ToList();
    }   
              
    public static GroupModel ToModel (this GroupEntity group){
        if (group is null){
            return null;
        }

        return new GroupModel{
            Id = group.Id,
            Name = group.Name,
            Users = group.Users,
            CreationDate = group.CreatedAt
        };
    }
}