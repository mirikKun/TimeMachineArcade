using System.Linq;
using Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private static readonly ColorType _startCarColor = ColorType.Blue;

    private readonly ColorType[] _startColors = new[]
    {
      ColorType.Blue,
      ColorType.Green
    };

    private readonly AccessoriesType[] _startAccessoriesTypes = new[]
    {
      AccessoriesType.None,
      AccessoriesType.PoliceSign,
      AccessoriesType.TaxiSign
    };
    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
      ISaveLoadService saveLoadService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
      LoadProgressOrInitNew();
      _gameStateMachine.Enter<LoadRoomSceneState>();
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew() =>
      _progressService.PlayerData =
        _saveLoadService.LoadProgress()
        ?? NewProgress();

    private PlayerData NewProgress()
    {
      PlayerData newProgress = new PlayerData();
      newProgress.CustomCarData.CarColor = _startCarColor;
      newProgress.CustomCarData.AccessoriesType = AccessoriesType.None;
      newProgress.CustomCarData.AvailableColors = _startColors.ToList();
      newProgress.CustomCarData.AvailableAccessories = _startAccessoriesTypes.ToList();
      return newProgress;
    }
  }
}