namespace keepr.Models;

public class VaultKeep : DBItem
{
  // public int Id { get; set; }
  // public string CreatorId { get; set; }
  public int VaultId { get; set; }
  public int KeepId { get; set; }
  public bool IsPrivate { get; set; }
}