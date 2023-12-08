namespace keepr.Repositories;

public class VaultKeepsRepo
{
  private readonly IDbConnection db;

  public VaultKeepsRepo(IDbConnection _db)
  {
    db = _db;
  }
  private KeepWithVaultKeepId PopulateCreator(KeepWithVaultKeepId keep, Profile profile)
  {
    keep.Creator = profile;
    return keep;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  { return db.QueryFirstOrDefault<VaultKeep>("SELECT * FROM vaultkeeps WHERE id = @VaultKeepId;", new { vaultKeepId }); }

  internal List<KeepWithVaultKeepId> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
        SELECT
        k.*,
        vk.id AS vaultKeepId,
        acc.*
        FROM keeps k
        JOIN vaultkeeps vk ON vk.keepId = k.id
        JOIN accounts acc ON acc.id = k.creatorId
        WHERE vk.vaultId = @VaultId;";
    return db.Query<KeepWithVaultKeepId, Profile, KeepWithVaultKeepId>(sql, PopulateCreator, new { vaultId }).ToList();
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"INSERT INTO 
        vaultkeeps(creatorId, vaultId, keepId)
        VALUES(@CreatorId, @VaultId, @KeepId);
        
        SELECT * FROM vaultkeeps WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<VaultKeep>(sql, vaultKeepData);
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vaultkeeps WHERE id = @vaultKeepId;";
    db.Execute(sql, new { vaultKeepId });
  }

}