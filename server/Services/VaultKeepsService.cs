namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepo vaultKeepsRepo;
  private readonly VaultsRepo vaultsRepo;
  private readonly KeepsRepo keepsRepo;

  public VaultKeepsService(VaultKeepsRepo _vaultKeepsRepo, VaultsRepo _vaultsRepo, KeepsRepo _keepsRepo)
  {
    vaultKeepsRepo = _vaultKeepsRepo;
    vaultsRepo = _vaultsRepo;
    keepsRepo = _keepsRepo;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  { return vaultKeepsRepo.GetVaultKeepById(vaultKeepId) ?? throw new Exception("Unable to locate by Id: " + vaultKeepId); }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    Vault vault = vaultsRepo.GetVaultById(vaultKeepData.VaultId);
    if (vault.CreatorId != vaultKeepData.CreatorId) { throw new Exception("Not your vault to add to!"); }
    VaultKeep vaultKeep = vaultKeepsRepo.CreateVaultKeep(vaultKeepData);
    // keepsRepo.AddKeptCount();
    return vaultKeep;
  }

  internal string DeleteVaultKeep(string creatorId, int vaultKeepId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != creatorId) { throw new Exception("Forbidden action: Not yours to delete"); }
    vaultKeepsRepo.DeleteVaultKeep(vaultKeepId);
    return "Vault-Keep Association has been permanently deleted";
  }

}