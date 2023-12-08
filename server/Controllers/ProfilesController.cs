namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService profilesService;

  public ProfilesController(ProfilesService _profilesService)
  {
    profilesService = _profilesService;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfileById(string profileId)
  {
    try { return Ok(profilesService.GetProfileById(profileId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{profileId}/vaults")]
  public ActionResult<Profile> GetVaultsByProfileId(string profileId)
  {
    try { return Ok(profilesService.GetVaultsByProfileId(profileId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<Profile> GetKeepsByProfileId(string profileId)
  {
    try { return Ok(profilesService.GetKeepsByProfileId(profileId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}