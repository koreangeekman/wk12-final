namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepo vaultKeepsRepo;

  public VaultKeepsService(VaultKeepsRepo _vaultKeepsRepo)
  {
    vaultKeepsRepo = _vaultKeepsRepo;
  }

  internal List<VaultKeep> GetKeepsByVaultId(int vaultKeepId)
  { return vaultKeepsRepo.GetKeepsByVaultId(vaultKeepId); }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  { return vaultKeepsRepo.GetVaultKeepById(vaultKeepId) ?? throw new Exception("Unable to locate by Id: " + vaultKeepId); }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  { return vaultKeepsRepo.CreateVaultKeep(vaultKeepData); }

  internal string DeleteVaultKeep(string creatorId, int vaultKeepId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != creatorId) { throw new Exception("Forbidden action: Not yours to delete"); }
    vaultKeepsRepo.DeleteVaultKeep(vaultKeepId);
    return "Vault-Keep Association has been permanently deleted";
  }

}