using DEVCoursesAPI.Data.DTOs;
using DEVCoursesAPI.Data.Models;
using Microsoft.Extensions.Options;

namespace DEVCoursesAPI.Services;

public interface IUsersService
{
    Guid Add(CreateUser user);
    bool Update(Users user);
    IList<Users> GetAll();


}