
namespace keepr.Repositories;

public class KeepsRepo
{
  private readonly IDbConnection db;

  public KeepsRepo(IDbConnection _db)
  {
    db = _db;
  }

  private int FormatKeeps()
  {
    return 2;
  }

  internal List<Keep> GetKeeps()
  { return db.Query<Keep>("SELECT * FROM keeps;").ToList(); }

  internal Keep GetKeepById(int keepId)
  { return db.QueryFirstOrDefault<Keep>("SELECT * FROM keeps WHERE id = @KeepId;", new { keepId }); }

  internal List<Keep> GetKeepsByQuery(string query)
  { return db.Query<Keep>("SELECT * FROM keeps WHERE name LIKE @Query;", new { query }).ToList(); }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"INSERT INTO 
        keeps(creatorId, name, description, img, views)
        VALUES(@CreatorId, @Name, @Description, @Img, @Views);
        
        SELECT * FROM keeps WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<Keep>(sql, keepData);
  }

  internal Keep EditKeep(Keep keepData)
  {
    string sql = @"
        UPDATE keeps
        SET
          name = @Name,
          description = @Description,
          img = @Img
        WHERE id = @Id
        
        SELECT * FROM keeps WHERE id = @Id;";
    return db.QueryFirstOrDefault<Keep>(sql, keepData);
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @KeepId;";
    db.QueryFirstOrDefault<Keep>(sql, new { keepId });
  }

}