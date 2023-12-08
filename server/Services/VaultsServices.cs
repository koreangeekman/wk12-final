namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepo vaultsRepo;

  public VaultsService(VaultsRepo _vaultsRepo)
  {
    vaultsRepo = _vaultsRepo;
  }

  internal Vault GetVaultById(int vaultId)
  { return vaultsRepo.GetVaultById(vaultId) ?? throw new Exception("Unable to locate by Id: " + vaultId); }

  internal Vault CreateVault(Vault vaultData)
  { return vaultsRepo.CreateVault(vaultData); }

  internal Vault EditVault(Vault vaultData)
  {
    Vault vault = GetVaultById(vaultData.Id);
    if (vault.CreatorId != vaultData.CreatorId) { throw new Exception("Forbidden action: Not yours to edit"); }
    vault.Name = vaultData.Name ?? vault.Name;
    vault.Description = vaultData.Description ?? vault.Description;
    vault.Img = vaultData.Img ?? vault.Img;
    vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;
    return vaultsRepo.EditVault(vault);
  }

  internal string DeleteVault(string creatorId, int vaultId)
  {
    Vault vault = GetVaultById(vaultId);
    if (vault.CreatorId != creatorId) { throw new Exception("Forbidden action: Not yours to delete"); }
    vaultsRepo.DeleteVault(vaultId);
    return "Vault has been permanently deleted";
  }

}