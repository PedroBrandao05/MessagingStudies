using AuthorRegistration.Entities;
using AuthorRegistration.Infra.Database.Models;
using AuthorRegistration.Infra.Repositories.Contracts;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthorRegistration.Infra.Repositories;

public class AuthorRepository : BaseRepository<Author, AuthorModel>, IAuthorRepository
{
  public AuthorRepository(DbContext dbContext) : base(dbContext)
  {
  }

  protected override AuthorModel ToDatabaseModel(Author entity)
  {
    return new AuthorModel().FromEntity(entity);
  }
}