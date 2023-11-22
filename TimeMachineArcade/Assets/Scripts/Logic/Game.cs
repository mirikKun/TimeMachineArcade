using UI.GameLevel;
using UI.Mediators;
using UnityEngine;
using Zenject;

namespace Logic
{
   public class Game : MonoBehaviour
   {
      [SerializeField] private int _totalSecondsTime;
      [SerializeField] private UiPoints _uiPoints;
      [SerializeField] private UiTimer _uiTimer;
      [SerializeField] private float _pointsPerSecond;

      private PointsCounter _pointsCounter;
   
      private IGameMediator _mediator;
      private CarMover _carMover;
   
      private float _currentTime;
      private bool _timeEnded;
      private GameEndReward _gameEndReward;

      [Inject]
      private void Construct(IGameMediator mediator,GameEndReward gameEndReward)
      {
         _mediator = mediator;
         _gameEndReward = gameEndReward;
      }

      public void SetPlayer(CarMover carMover)
      {
         _carMover = carMover;
         _carMover.OnDrifting += AddPoints;
      }

      private void OnDestroy()
      {
         if(_carMover)
            _carMover.OnDrifting -= AddPoints;
      }

      private void Start()
      {
         ResetGame();
      }

      private void Update()
      {
         if(_timeEnded)
            return;
         _currentTime += Time.deltaTime;
         if (_currentTime >= _totalSecondsTime)
         {
            EndGame();
         }
         _uiTimer.UpdateTimer(_currentTime);
      }

      private void AddPoints()
      {
         _pointsCounter.AddDriftingPoints(Time.deltaTime);
         _uiPoints.UpdatePointsText(_pointsCounter.CurrentPoints);
      }
      public void ResetGame()
      {
         _currentTime = 0;
         _pointsCounter = new PointsCounter(_pointsPerSecond);
         if(_carMover)
            _carMover.Reset();
         _uiTimer.UpdateTimer(_currentTime);
         _uiPoints.UpdatePointsText(_pointsCounter.CurrentPoints);


      }
      private void EndGame()
      {
         _currentTime = _totalSecondsTime;
         _timeEnded = true;
         _mediator.OpenGameEndPanel();
         _gameEndReward.InitializeReward((int)_pointsCounter.CurrentPoints);
      }
   }
}