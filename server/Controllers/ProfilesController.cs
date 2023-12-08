namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly Auth0Provider a0;
  private readonly ProfilesService profilesService;

  public ProfilesController(ProfilesService _profilesService, Auth0Provider _a0)
  {
    a0 = _a0;
    profilesService = _profilesService;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfileById(string profileId)
  {
    try { return Ok(profilesService.GetProfileById(profileId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<Profile>> GetVaultsByProfileId(string profileId)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(profilesService.GetVaultsByProfileId(userInfo?.Id, profileId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<Profile> GetKeepsByProfileId(string profileId)
  {
    try { return Ok(profilesService.GetKeepsByProfileId(profileId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}