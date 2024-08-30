using AuthorRegistration.Entities;
using Primitives;

namespace AuthorRegistration.Infra.Database.Models;

public class AuthorModel : DatabaseModel<Author>
{
  public string Name { get; set; } = String.Empty;
  
  public string? Picture { get; set; }
  
  public string? Nationality { get; set; }
  
  public string? Biography { get; set; }
  
  public override Author ToEntity()
  {
    return new Author
    {
      Id = Id,
      Name = Name,
      Biography = Biography,
      Nationality = Nationality,
      Picture = Picture
    };
  }

  public override AuthorModel FromEntity(Author entity)
  {
    return new AuthorModel
    {
      Id = entity.Id,
      Biography = entity.Biography,
      Name = entity.Name,
      Nationality = entity.Nationality,
      Picture = entity.Picture
    };
  }
}