namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly Auth0Provider a0;
  private readonly VaultsService vaultsService;
  private readonly VaultKeepsService vaultKeepsService;

  public VaultsController(Auth0Provider _a0, VaultsService _vaultsService, VaultKeepsService _vaultKeepsService)
  {
    a0 = _a0;
    vaultsService = _vaultsService;
    vaultKeepsService = _vaultKeepsService;
  }

  [HttpGet("{vaultId}")]
  public ActionResult<Vault> GetVaultById(int vaultId)
  {
    try { return Ok(vaultsService.GetVaultById(vaultId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }


  [HttpGet("{vaultId}/keeps")]
  public ActionResult<List<VaultKeep>> GetKeepsByVaultId(int vaultId)
  {
    try { return Ok(vaultKeepsService.GetKeepsByVaultId(vaultId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault(Vault vaultData)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      return Ok(vaultsService.CreateVault(vaultData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpPut("{vaultId}")]
  public async Task<ActionResult<Vault>> EditVault(int vaultId, Vault vaultData)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      vaultData.Id = vaultId;
      return Ok(vaultsService.EditVault(vaultData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{vaultId}")]
  public async Task<ActionResult<Vault>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(vaultsService.DeleteVault(userInfo.Id, vaultId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}