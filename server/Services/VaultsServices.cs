namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepo vaultsRepo;
  private readonly VaultKeepsRepo vaultKeepsRepo;

  public VaultsService(VaultsRepo _vaultsRepo, VaultKeepsRepo _vaultKeepsRepo)
  {
    vaultsRepo = _vaultsRepo;
    vaultKeepsRepo = _vaultKeepsRepo;
  }

  internal Vault GetVaultById(string creatorId, int vaultId)
  {
    Vault vault = vaultsRepo.GetVaultById(vaultId) ?? throw new Exception("Unable to locate by Id: " + vaultId);
    if (vault.IsPrivate == true && vault.CreatorId != creatorId) { throw new Exception("Unable to locate by Id:: " + vaultId); }
    return vault;
  }

  internal List<KeepWithVaultKeepId> GetKeepsByVaultId(string accountId, int vaultId)
  {
    GetVaultById(accountId, vaultId);
    return vaultKeepsRepo.GetKeepsByVaultId(vaultId);
  }

  internal List<Vault> GetPublicVaults(string accountId, int vaultId)
  {
    if (accountId == null)
    { return vaultsRepo.GetPublicVaults(); }
    return vaultsRepo.GetMyPrivateAndAllPublicVaults(accountId);
  }

  internal Vault CreateVault(Vault vaultData)
  { return vaultsRepo.CreateVault(vaultData); }

  internal Vault EditVault(Vault vaultData)
  {
    Vault vault = GetVaultById(vaultData.CreatorId, vaultData.Id);
    if (vault.CreatorId != vaultData.CreatorId) { throw new Exception("Forbidden action: Not yours to edit"); }
    vault.Name = vaultData.Name ?? vault.Name;
    vault.Description = vaultData.Description ?? vault.Description;
    vault.Img = vaultData.Img ?? vault.Img;
    vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;
    return vaultsRepo.EditVault(vault);
  }

  internal string DeleteVault(string creatorId, int vaultId)
  {
    Vault vault = GetVaultById(creatorId, vaultId);
    if (vault.CreatorId != creatorId) { throw new Exception("Forbidden action: Not yours to delete"); }
    vaultsRepo.DeleteVault(vaultId);
    return "Vault has been permanently deleted";
  }

}