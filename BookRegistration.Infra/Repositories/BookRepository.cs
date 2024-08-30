using BookRegistration.Entities;
using BookRegistration.Infra.Database.Models;
using BookRegistration.Infra.Repositories.Contracts;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace BookRegistration.Infra.Repositories;

public class BookRepository : BaseRepository<Book, BookModel>, IBookRepository
{
  public BookRepository(DbContext dbContext) : base(dbContext)
  {
  }

  protected override BookModel ToDatabaseModel(Book entity)
  {
    return new BookModel().FromEntity(entity);
  }
}