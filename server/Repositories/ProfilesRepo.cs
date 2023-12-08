namespace keepr.Repositories;

public class ProfilesRepo
{
  private readonly IDbConnection db;

  public ProfilesRepo(IDbConnection _db)
  {
    db = _db;
  }

  internal Profile GetProfileById(string profileId)
  { return db.QueryFirstOrDefault<Profile>("SELECT * FROM accounts WHERE id = @ProfileId;", new { profileId }); }

}