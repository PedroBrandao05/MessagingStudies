using BookRegistration.Entities;
using Primitives;

namespace BookRegistration.Infra.Database.Models;

public class BookModel : DatabaseModel<Book>
{
  public string Name { get; set; } = String.Empty;
  
  public string Description { get; set; } = String.Empty;
  
  public string? Thumbnail { get; set; } = String.Empty;
  
  public Guid? AuthorId { get; set; }
  
  public override Book ToEntity()
  {
    return new Book
    {
      Description = Description,
      AuthorId = AuthorId,
      Thumbnail = Thumbnail,
      Name = Name,
      Id = Id
    };
  }

  public override BookModel FromEntity(Book entity)
  {
    return new BookModel
    {
      Description = entity.Description,
      AuthorId = entity.AuthorId,
      Name = entity.Name,
      Thumbnail = entity.Thumbnail,
      Id = entity.Id
    };
  }
}