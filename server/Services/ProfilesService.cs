namespace keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepo profilesRepo;
  private readonly VaultsRepo vaultsRepo;
  private readonly KeepsRepo keepsRepo;

  public ProfilesService(
    ProfilesRepo _profilesRepo,
    VaultsRepo _vaultsRepo,
    KeepsRepo _keepsRepo
    )
  {
    profilesRepo = _profilesRepo;
    vaultsRepo = _vaultsRepo;
    keepsRepo = _keepsRepo;
  }

  internal Profile GetProfileById(string profileId)
  { return profilesRepo.GetProfileById(profileId); }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  { return vaultsRepo.GetVaultsByProfileId(profileId); }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  { return keepsRepo.GetKeepsByProfileId(profileId); }

}