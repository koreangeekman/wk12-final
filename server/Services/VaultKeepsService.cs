namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepo vaultKeepsRepo;

  public VaultKeepsService(VaultKeepsRepo _vaultKeepsRepo)
  {
    vaultKeepsRepo = _vaultKeepsRepo;
  }
}