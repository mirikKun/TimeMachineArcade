using Data;

namespace Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService 
  {
    void SaveProgress();
    PlayerData LoadProgress();
  }
}