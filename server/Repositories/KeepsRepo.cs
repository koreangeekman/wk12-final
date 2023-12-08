
namespace keepr.Repositories;

public class KeepsRepo
{
  private readonly IDbConnection db;

  public KeepsRepo(IDbConnection _db)
  {
    db = _db;
  }

  private Keep PopulateCreator(Keep keep, Profile profile)
  {
    keep.Creator = profile;
    return keep;
  }


  internal List<Keep> GetKeeps()
  {
    string sql = @"
        SELECT 
        k.*,
        acc.*
        FROM keeps k
        JOIN accounts acc ON acc.id = k.creatorId;";
    return db.Query<Keep, Profile, Keep>(sql, PopulateCreator).ToList();
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
        SELECT 
        k.*,
        acc.*
        FROM keeps k
        JOIN accounts acc ON acc.id = k.creatorId
        WHERE k.id = @keepId;";
    return db.Query<Keep, Profile, Keep>(sql, PopulateCreator, new { keepId }).FirstOrDefault();
  }

  internal List<Keep> GetKeepsByProfileId(string accountId)
  { return db.Query<Keep>("SELECT * FROM keeps WHERE creatorId = @AccountId;", new { accountId }).ToList(); }

  internal List<Keep> GetKeepsByQuery(string query)
  { return db.Query<Keep>("SELECT * FROM keeps WHERE name OR description LIKE @Query;", new { query }).ToList(); }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"INSERT INTO 
        keeps(creatorId, name, description, img)
        VALUES(@CreatorId, @Name, @Description, @Img);
        
        SELECT 
        k.*,
        acc.* 
        FROM keeps k
        JOIN accounts acc ON acc.id = k.creatorId  
        WHERE k.id = LAST_INSERT_ID();";
    return db.Query<Keep, Profile, Keep>(sql, PopulateCreator, keepData).FirstOrDefault();
  }

  internal Keep EditKeep(Keep keepData)
  {
    string sql = @"
        UPDATE keeps
        SET
          name = @Name,
          description = @Description,
          img = @Img,
          views = @Views,
          kept = @Kept
        WHERE id = @Id;
        
        SELECT 
        k.*,
        acc.* 
        FROM keeps k
        JOIN accounts acc ON acc.id = k.creatorId  
        WHERE k.id = @Id;";
    return db.Query<Keep, Profile, Keep>(sql, PopulateCreator, keepData).FirstOrDefault();
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @KeepId;";
    db.Execute(sql, new { keepId });
  }

}