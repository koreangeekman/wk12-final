namespace keepr.Repositories;

public class VaultsRepo
{
  private readonly IDbConnection db;

  public VaultsRepo(IDbConnection _db)
  {
    db = _db;
  }

}