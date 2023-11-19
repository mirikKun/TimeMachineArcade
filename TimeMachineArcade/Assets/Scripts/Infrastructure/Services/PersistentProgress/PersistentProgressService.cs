using Data;

namespace Infrastructure.Services.PersistentProgress
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerData PlayerData { get; set; }
  }
}