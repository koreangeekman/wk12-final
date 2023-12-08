namespace keepr.Models;

public class Keep : DBItem
{
  // public int Id { get; set; }
  // public string CreatorId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public bool IsPrivate { get; set; }
}