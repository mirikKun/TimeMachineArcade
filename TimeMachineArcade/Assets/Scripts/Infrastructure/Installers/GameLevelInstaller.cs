using Infrastructure.Fabric;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameLevelInstaller:MonoInstaller
    {
        [SerializeField] private Game _game;
        [SerializeField] private GameSingleLevelMediator _mediator;
        [SerializeField] private GameEndReward _gameEndReward;

        [SerializeField] private Transform _spawnPoint;
        private IGameFactory _gameFactory;
        
        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        public override void InstallBindings()
        {
            BindGameEndRewarder();
            CarMover carMover = BindPlayer();
            BindGame(carMover);
            BindMediator();
        }

        private void BindGameEndRewarder()
        {
            Container
                .Bind<GameEndReward>()
                .FromInstance(_gameEndReward)
                .AsSingle();    
        }
        private void BindGame(CarMover carMover)
        {
            Container
                .Bind<Game>()
                .FromInstance(_game)
                .AsSingle();
            _game.SetPlayer(carMover);
        }

        private CarMover BindPlayer()
        {
            GameObject car = _gameFactory.CreatePlayer(_spawnPoint);
            CarMover carMover = car.GetComponent<CarMover>();
            Container
                .Bind<CarMover>()
                .FromInstance(carMover);
            return carMover;
        }

        private void BindMediator()
        {
            Container
                .Bind<GameSingleLevelMediator>()
                .FromInstance(_mediator)
                .AsSingle();
            Container.Bind<IGameMediator>().FromInstance(_mediator);

        }
    }
}