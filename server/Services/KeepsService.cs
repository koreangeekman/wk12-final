namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepo keepsRepo;

  public KeepsService(KeepsRepo _keepsRepo)
  {
    keepsRepo = _keepsRepo;
  }

  internal List<Keep> GetKeeps(string query)
  {
    if (query != null)
    {
      return keepsRepo.GetKeepsByQuery(query);
    }
    return keepsRepo.GetKeeps();
  }

  internal Keep GetKeepById(int keepId)
  { return keepsRepo.GetKeepById(keepId) ?? throw new Exception("Unable to locate by Id: " + keepId); }

  internal Keep CreateKeep(Keep keepData)
  { return keepsRepo.CreateKeep(keepData); }

  internal Keep EditKeep(Keep keepData)
  {
    Keep keep = GetKeepById(keepData.Id);
    if (keep.CreatorId != keepData.CreatorId) { throw new Exception("Forbidden action: Not yours to edit"); }
    keep.Name = keepData.Name ?? keep.Name;
    keep.Description = keepData.Description ?? keep.Description;
    keep.Img = keepData.Img ?? keep.Img;
    return keepsRepo.EditKeep(keepData);
  }

  internal string DeleteKeep(string creatorId, int keepId)
  {
    keepsRepo.DeleteKeep(keepId);
    return "Keep has been permanently deleted";
  }


}