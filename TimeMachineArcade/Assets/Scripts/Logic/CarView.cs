using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class CarView : MonoBehaviour
    {
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private Transform _accessoriesHolder;
        private IAssetProvider _assetProvider;
        private IPersistentProgressService _progressService;

    
        [Inject]
        private void Construct(IAssetProvider assetProvider,IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _progressService = progressService;
        }

        private void Start()
        {
            CustomCarData currentCarData = _progressService.PlayerData.CustomCarData;
            ChangeColor(currentCarData.CarColor.GetColor());
            CreateAccessory(currentCarData.AccessoriesType);
 
        }


        private void ChangeColor(Color color)
        {
            foreach (Renderer renderer in _renderers)
            {
                renderer.material.SetColor("_Color", color);
            }
        }
        private void CreateAccessory(AccessoriesType type)
        {
            if(type==AccessoriesType.None)
                return;
            GameObject newAccessories = _assetProvider.Instantiate(type.ToString());
            newAccessories.transform.SetParent(_accessoriesHolder);
            newAccessories.transform.localPosition = Vector3.zero;
            newAccessories.transform.localRotation = Quaternion.identity;
        }
    }
}
