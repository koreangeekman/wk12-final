namespace keepr.Repositories;

public class VaultKeepsRepo
{
  private readonly IDbConnection db;

  public VaultKeepsRepo(IDbConnection _db)
  {
    db = _db;
  }

  internal List<VaultKeep> GetKeepsByVaultId(int vaultId)
  { return db.Query<VaultKeep>("SELECT * FROM vaultkeeps WHERE vaultId = @VaultId;", new { vaultId }).ToList(); }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  { return db.QueryFirstOrDefault<VaultKeep>("SELECT * FROM vaultkeeps WHERE id = @VaultKeepId;", new { vaultKeepId }); }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"INSERT INTO 
        vaults(creatorId, vaultId, keepId)
        VALUES(@CreatorId, @VaultId, @KeepId);
        
        SELECT * FROM vaultkeeps WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<VaultKeep>(sql, vaultKeepData);
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vaultkeeps WHERE id = @VaultId;";
    db.Execute(sql, new { vaultKeepId });
  }

}