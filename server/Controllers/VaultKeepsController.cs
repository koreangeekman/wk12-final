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
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep(VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      return Ok(vaultKeepsService.CreateVaultKeep(vaultKeepData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{vaultKeepId}")]
  public async Task<ActionResult<VaultKeep>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(vaultKeepsService.DeleteVaultKeep(userInfo.Id, vaultKeepId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}