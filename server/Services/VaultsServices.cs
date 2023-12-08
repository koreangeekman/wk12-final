namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepo vaultsRepo;

  public VaultsService(VaultsRepo _vaultsRepo)
  {
    vaultsRepo = _vaultsRepo;
  }
}