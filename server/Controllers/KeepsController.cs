namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly Auth0Provider a0;
  private readonly KeepsService keepsService;

  public KeepsController(Auth0Provider _a0, KeepsService _keepsService)
  {
    a0 = _a0;
    keepsService = _keepsService;
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetKeeps(string query)
  {
    try { return Ok(keepsService.GetKeeps(query)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try { return Ok(keepsService.GetKeepById(keepId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep(Keep keepData)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      return Ok(keepsService.CreateKeep(keepData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpPut("{keepId}")]
  public async Task<ActionResult<Keep>> EditKeep(int keepId, Keep keepData)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      keepData.Id = keepId;
      return Ok(keepsService.EditKeep(keepData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{keepId}")]
  public async Task<ActionResult<Keep>> DeleteKeep(int keepId)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(keepsService.DeleteKeep(userInfo.Id, keepId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}