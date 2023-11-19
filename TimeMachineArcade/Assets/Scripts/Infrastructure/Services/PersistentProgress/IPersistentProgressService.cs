using Data;

namespace Infrastructure.Services.PersistentProgress
{
  public interface IPersistentProgressService 
  {
    PlayerData PlayerData { get; set; }
  }
}