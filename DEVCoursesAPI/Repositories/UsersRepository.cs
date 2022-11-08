using DEVCoursesAPI.Data.Context;
using DEVCoursesAPI.Data.DTOs;
using DEVCoursesAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DEVCoursesAPI.Repositories;

public class UsersRepository : IUsersRepository<Users>
{
    private readonly IDbContextFactory<DEVCoursesContext> _dbContextFactory;

    public UsersRepository(IDbContextFactory<DEVCoursesContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }


    public Guid Add(Users model)
    {
        throw new NotImplementedException();
    }

    public bool Update(Users model)
    {
        throw new NotImplementedException();
    }

    public IList<Users> GetAll()
    {
        throw new NotImplementedException();
    }
}
    