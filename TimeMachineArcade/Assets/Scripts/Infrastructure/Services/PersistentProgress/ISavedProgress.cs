using Data;

namespace Infrastructure.Services.PersistentProgress
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerData data);
  }

  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerData data);
  }
}