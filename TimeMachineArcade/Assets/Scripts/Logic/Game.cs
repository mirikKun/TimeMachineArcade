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

      private readonly CoinsCounter _coinsCounter= new();
      private CoinsAnimator _coinsAnimator;
   
      private IGameMediator _mediator;
      private CarMover _carMover;
   
      private float _currentTime;
      private bool _timeEnded;
      private GameEndReward _gameEndReward;
      private LevelGenerator _levelGenerator;
      private TransitionEffect _transitionEffect;

      [Inject]
      private void Construct(IGameMediator mediator,TransitionEffect transitionEffect,CoinsAnimator coinsAnimator,GameEndReward gameEndReward,LevelGenerator levelGenerator)
      {
         _transitionEffect = transitionEffect;
         _coinsAnimator = coinsAnimator;
         _mediator = mediator;
         _levelGenerator = levelGenerator;
         _gameEndReward = gameEndReward;
      }

      public void SetPlayer(CarMover carMover)
      {
         _carMover = carMover;
         _carMover.OnObstacleHit += OnObstacleHit;
         _carMover.OnPortalEnter += ChangeSetting;

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

      private void ChangeSetting()
      {
         _transitionEffect.AnimateTransition(_carMover.transform.position);
         _carMover.MoveToStartPoint();
         _carMover.GetComponent<WheelTrailsSwitch>().ClearTrails();

         _levelGenerator.ChangeSetting();
      }
      private void OnObstacleHit(int coins, Vector3 position,bool drifting)
      {
         if (drifting)
         {
            coins *=2;
         }
         _coinsAnimator.PlayCoinsAnimation(coins,position);
         _coinsCounter.AddActualCoins(coins); 
      }
 
      public void ResetGame()
      {
         _timeEnded = false;
         _currentTime = 0;
         _coinsCounter.ResetCoins();
         _coinsAnimator.Init(_coinsCounter);
         
         if(_carMover)
         {
            _carMover.Reset();
            _carMover.GetComponent<WheelTrailsSwitch>().ClearTrails();
         }         
         _uiTimer.InitTimer(_totalSecondsTime);
         _uiTimer.UpdateTimer(_currentTime);
         
         _levelGenerator.SetStartSetting();
         _levelGenerator.ResetAll();
      }
      private void EndGame()
      {
         _currentTime = _totalSecondsTime;
         _timeEnded = true;
         _mediator.OpenGameEndPanel();
         _gameEndReward.InitializeReward(_coinsCounter.ActualCurrentCoins);
      }
   }
}