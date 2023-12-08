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
  public ActionResult<List<KeepsController>> GetKeeps()
  {
    try { return Ok(); }
    catch (Exception e) { return BadRequest(e.Message); }
  }
}