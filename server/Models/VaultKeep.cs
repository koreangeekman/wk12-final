namespace keepr.Models;

public class VaultKeep : DBItem
{
  // public int Id { get; set; }
  // public string CreatorId { get; set; }
  public string VaultId { get; set; }
  public string KeepId { get; set; }
  public bool IsPrivate { get; set; }
}