namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly Auth0Provider a0;
  private readonly VaultsService vaultsService;

  public VaultsController(Auth0Provider _a0, VaultsService _vaultsService)
  {
    a0 = _a0;
    vaultsService = _vaultsService;
  }

  // [HttpGet]
  // public ActionResult<List<VaultsController>> GetVaults()
  // {
  //   try { return Ok(vaultsService.GetVaults()); }
  //   catch (Exception e) { return BadRequest(e.Message); }
  // }
}