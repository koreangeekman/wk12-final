namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider a0;
  private readonly VaultKeepsService vaultKeepsService;

  public VaultKeepsController(Auth0Provider _a0, VaultKeepsService _vaultKeepsService)
  {
    a0 = _a0;
    vaultKeepsService = _vaultKeepsService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVault(VaultKeep vaultData)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      return Ok(vaultKeepsService.CreateVaultKeep(vaultData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{vaultId}")]
  public async Task<ActionResult<VaultKeep>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(vaultKeepsService.DeleteVaultKeep(userInfo.Id, vaultId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}