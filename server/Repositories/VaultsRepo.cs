namespace keepr.Repositories;

public class VaultsRepo
{
  private readonly IDbConnection db;

  public VaultsRepo(IDbConnection _db)
  {
    db = _db;
  }

  private int FormatVaults()
  {
    return 2;
  }

  internal Vault GetVaultById(int vaultId)
  { return db.QueryFirstOrDefault<Vault>("SELECT * FROM vaults WHERE id = @VaultId;", new { vaultId }); }

  internal List<Vault> GetVaultsByProfileId(string creatorId)
  { return db.Query<Vault>("SELECT * FROM vaults WHERE creatorId = @CreatorId;", new { creatorId }).ToList(); }

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"INSERT INTO 
        vaults(creatorId, name, description, img, isPrivate)
        VALUES(@CreatorId, @Name, @Description, @Img, @IsPrivate);
        
        SELECT * FROM vaults WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<Vault>(sql, vaultData);
  }

  internal Vault EditVault(Vault vaultData)
  {
    string sql = @"
        UPDATE vaults
        SET
          name = @Name,
          description = @Description,
          img = @Img,
          isPrivate = @IsPrivate
        WHERE id = @Id;
        
        SELECT * FROM vaults WHERE id = @Id;";
    return db.QueryFirstOrDefault<Vault>(sql, vaultData);
  }

  internal void DeleteVault(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE id = @VaultId;";
    db.Execute(sql, new { vaultId });
  }

}