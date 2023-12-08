namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService accountService;
  private readonly Auth0Provider a0;

  public AccountController(AccountService _accountService, Auth0Provider _a0)
  {
    accountService = _accountService;
    a0 = _a0;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpGet("vaults")]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(accountService.GetMyVaults(userInfo.Id));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}
