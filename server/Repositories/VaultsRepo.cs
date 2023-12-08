namespace keepr.Repositories;

public class VaultsRepo
{
  private readonly IDbConnection db;

  public VaultsRepo(IDbConnection _db)
  {
    db = _db;
  }

  private Vault PopulateCreator(Vault vault, Profile profile)
  {
    vault.Creator = profile;
    return vault;
  }

  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
        SELECT 
        v.*,
        acc.*
        FROM vaults v
        JOIN accounts acc ON acc.id = v.creatorId
        WHERE v.id = @VaultId;";
    return db.Query<Vault, Profile, Vault>(sql, PopulateCreator, new { vaultId }).FirstOrDefault();
  }

  internal List<Vault> GetAllVaultsByProfileId(string profileId)
  {
    string sql = "SELECT * FROM vaults WHERE creatorId = @ProfileId;";
    return db.Query<Vault>(sql, new { profileId }).ToList();
  }

  internal List<Vault> GetPublicVaultsByProfileId(string profileId)
  {
    string sql = @"
        SELECT * FROM vaults 
        WHERE creatorId = @ProfileId
        AND isPrivate = false;";
    return db.Query<Vault>(sql, new { profileId }).ToList();
  }

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"INSERT INTO 
        vaults(creatorId, name, description, img, isPrivate)
        VALUES(@CreatorId, @Name, @Description, @Img, @IsPrivate);
        
        SELECT 
        v.*,
        acc.*
        FROM vaults v
        JOIN accounts acc ON acc.id = v.creatorId
        WHERE v.id = LAST_INSERT_ID();";
    return db.Query<Vault, Profile, Vault>(sql, PopulateCreator, vaultData).FirstOrDefault();
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