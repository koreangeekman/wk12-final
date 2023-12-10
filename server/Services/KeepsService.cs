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
    if (query == null) { return keepsRepo.GetKeeps(); }
    return keepsRepo.GetKeepsByQuery("'%" + query + "%'");
  }

  internal Keep GetKeepById(int keepId, bool alterKeep, string creatorId)
  {
    Keep keep = keepsRepo.GetKeepById(keepId) ?? throw new Exception("Unable to locate by Id: " + keepId);
    if (alterKeep) { return keep; }
    if (keep.Views > 0 && keep.CreatorId == creatorId) { return keep; }
    keep = AddView(keep);
    return keep;
  }

  internal Keep CreateKeep(Keep keepData)
  { return keepsRepo.CreateKeep(keepData); }

  internal Keep EditKeep(Keep keepData)
  {
    Keep keep = GetKeepById(keepData.Id, true, keepData.CreatorId);
    if (keep.CreatorId != keepData.CreatorId) { throw new Exception("Forbidden action: Not yours to edit"); }
    keep.Name = keepData.Name ?? keep.Name;
    keep.Description = keepData.Description ?? keep.Description;
    keep.Img = keepData.Img ?? keep.Img;
    return keepsRepo.EditKeep(keep);
  }

  internal string DeleteKeep(string creatorId, int keepId)
  {
    Keep keep = GetKeepById(keepId, true, creatorId);
    if (keep.CreatorId != creatorId) { throw new Exception("Forbidden action: Not yours to delete"); }
    keepsRepo.DeleteKeep(keepId);
    return "Keep has been permanently deleted";
  }

  private Keep AddView(Keep keep)
  {
    keep.Views++;
    return keepsRepo.EditKeep(keep);
  }

  internal Keep AddKeptCount(int keepId)
  {
    Keep keep = GetKeepById(keepId, true, "");
    keep.Kept++;
    return keepsRepo.EditKeep(keep);
  }

  internal Keep RemoveKeptCount(int keepId)
  {
    Keep keep = GetKeepById(keepId, true, "");
    keep.Kept--;
    return keepsRepo.EditKeep(keep);
  }

}