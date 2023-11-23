using System;
using Logic.Generators;
using UI.GameLevel;
using UI.Mediators;
using UnityEngine;
using Zenject;

namespace Logic
{
   public class Game : MonoBehaviour
   {
      [SerializeField] private int _totalSecondsTime;
      [SerializeField] private UiCoins _uiCoins;
      [SerializeField] private UiTimer _uiTimer;

      private readonly CoinsCounter _coinsCounter= new CoinsCounter();
      private CoinsAnimator _coinsAnimator;
   
      private IGameMediator _mediator;
      private CarMover _carMover;
   
      private float _currentTime;
      private bool _timeEnded;
      private GameEndReward _gameEndReward;
      private LevelGenerator _levelGenerator;

      [Inject]
      private void Construct(IGameMediator mediator,CoinsAnimator coinsAnimator,GameEndReward gameEndReward,LevelGenerator levelGenerator)
      {
         _coinsAnimator = coinsAnimator;
         _mediator = mediator;
         _levelGenerator = levelGenerator;
         _gameEndReward = gameEndReward;
      }

      public void SetPlayer(CarMover carMover)
      {
         _carMover = carMover;
         _carMover.OnObstacleHit += OnObstacleHit;

      }

      private void OnEnable()
      {
         _coinsCounter.OnChanged += _uiCoins.UpdateCoinsText;

      }

      private void OnDestroy()
      {
         if(_carMover)
            _carMover.OnObstacleHit -= OnObstacleHit;
         _coinsCounter.OnChanged -= _uiCoins.UpdateCoinsText;

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

      private void OnObstacleHit(int coins, Vector3 position)
      {
         _coinsAnimator.PlayCoinsAnimation(coins,position);
      }
 
      public void ResetGame()
      {
         _currentTime = 0;
         _coinsCounter.ResetCoins();
         _coinsAnimator.Init(_coinsCounter);
         if(_carMover)
            _carMover.Reset();
         _uiTimer.UpdateTimer(_currentTime);

         _levelGenerator.ResetAll();
      }
      private void EndGame()
      {
         _currentTime = _totalSecondsTime;
         _timeEnded = true;
         _mediator.OpenGameEndPanel();
         _gameEndReward.InitializeReward((int)_coinsCounter.CurrentCoins);
      }
   }
}