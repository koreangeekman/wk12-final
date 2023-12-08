
namespace keepr.Services;

public class AccountService
{
  private readonly AccountsRepository repo;
  private readonly VaultsRepo vaultsRepo;

  public AccountService(AccountsRepository _repo, VaultsRepo _vaultsRepo)
  {
    repo = _repo;
    vaultsRepo = _vaultsRepo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
    return repo.Edit(original);
  }

  internal List<Vault> GetMyVaults(string accountId)
  { return vaultsRepo.GetAllVaultsByProfileId(accountId); }
}
