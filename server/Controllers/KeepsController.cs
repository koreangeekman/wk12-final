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
  public ActionResult<List<Keep>> GetKeeps()
  {
    try { return Ok(keepsService.GetKeeps()); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try { return Ok(keepsService.GetKeepById(keepId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetKeepsByQuery(string query)
  {
    try { return Ok(); }
    catch (Exception e) { return BadRequest(e.Message); }
  }


}