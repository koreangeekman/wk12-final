namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepo vaultKeepsRepo;
  private readonly VaultsRepo vaultsRepo;
  private readonly KeepsService keepsService;

  public VaultKeepsService(VaultKeepsRepo _vaultKeepsRepo, VaultsRepo _vaultsRepo, KeepsService _keepsService)
  {
    vaultKeepsRepo = _vaultKeepsRepo;
    vaultsRepo = _vaultsRepo;
    keepsService = _keepsService;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  { return vaultKeepsRepo.GetVaultKeepById(vaultKeepId) ?? throw new Exception("Unable to locate VK by Id: " + vaultKeepId); }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    Vault vault = vaultsRepo.GetVaultById(vaultKeepData.VaultId);
    if (vault.CreatorId != vaultKeepData.CreatorId) { throw new Exception("Not your vault to add a VK to!"); }
    VaultKeep vaultKeep = vaultKeepsRepo.CreateVaultKeep(vaultKeepData);
    keepsService.AddKeptCount(vaultKeep.KeepId);
    return vaultKeep;
  }

  internal string DeleteVaultKeep(string creatorId, int vaultKeepId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != creatorId) { throw new Exception("Forbidden action: Not your VK to delete"); }
    vaultKeepsRepo.DeleteVaultKeep(vaultKeepId);
    keepsService.RemoveKeptCount(vaultKeep.KeepId);
    return "Vault-Keep Association has been permanently deleted";
  }

}