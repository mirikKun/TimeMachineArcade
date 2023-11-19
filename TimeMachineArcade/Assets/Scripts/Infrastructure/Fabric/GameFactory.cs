using Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Infrastructure.Fabric
{
    public class GameFactory:IGameFactory
    {
        private readonly IAssetProvider _assets;
        private GameObject _playerGameObject;
        private readonly string _playerPath= "PlayerCar";
        private readonly string _playerOnlinePath= "PlayerCarOnline";
        private DiContainer _diContainer;
        [Inject]
        public GameFactory(DiContainer diContainer,IAssetProvider assets)
        {
            _assets = assets;
            _diContainer = diContainer;
        }
        public GameObject CreatePlayer(Transform at)
        {
            GameObject playerAsset=_assets.GetAsset(path: _playerPath);
            _playerGameObject=_diContainer.InstantiatePrefab(playerAsset, at.position, Quaternion.identity, at);
            return _playerGameObject;
        }

    }
}