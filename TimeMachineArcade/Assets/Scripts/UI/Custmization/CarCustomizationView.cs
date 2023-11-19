using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class CarCustomizationView : MonoBehaviour
    {
        [SerializeField] private ColorChooseButton[] _colorChooseButtons;
        [SerializeField] private ColorType _currentColor;
        private ColorType _temporaryColor;

        [SerializeField] private Renderer[] _renderers;

        [SerializeField] private AccessoriesChooseButton[] _accessoriesChooseButtons;
        [SerializeField] private AccessoriesType _currentAccessoriesType;
        private AccessoriesType _temporaryAccessoriesType;

        [SerializeField] private Transform _accessoriesHolder;

        [SerializeField] private Button _doneButton;
        private IAssetProvider _assetProvider;
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _progressService;
        private RoomMediator _roomMediator;

        [Inject]
        private void Construct(RoomMediator roomMediator,IAssetProvider assetProvider, ISaveLoadService saveLoadService,
            IPersistentProgressService progressService)
        {
            _roomMediator = roomMediator;
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        private void Start()
        {
            ColorButtonsInit();
            AccessoriesButtonsInit();
            SaveButtonInit();
            Reset();
            UpdateButtons();
        }

        public void UpdateButtons()
        {
            if (_progressService.PlayerData == null)
                return;
            SetActiveAvailableColorButtons();
            SetActiveAvailableAccessoryButtons();
        }

        public void Reset()
        {
            CustomCarData currentCarData = _progressService.PlayerData.CustomCarData;
            ChangeColor(currentCarData.CarColor);
            ChangeAccessories(currentCarData.AccessoriesType);
        }

        private void SaveButtonInit()
        {
            _doneButton.onClick.AddListener(SaveChanges);
        }

        private void ChangeColor(ColorType color)
        {
            _temporaryColor = color;
            foreach (Renderer renderer in _renderers)
            {
                renderer.material.SetColor("_Color", color.GetColor());
            }
        }

        public void ChangeAccessories(AccessoriesType type)
        {
            if (_temporaryAccessoriesType != AccessoriesType.None)
            {
                DeleteAllAccessories();
            }

            _temporaryAccessoriesType = type;


            CreateAccessory(type);
        }

        private void CreateAccessory(AccessoriesType type)
        {
            if (type == AccessoriesType.None)
                return;
            GameObject newAccessories = _assetProvider.Instantiate(type.ToString());
            newAccessories.transform.SetParent(_accessoriesHolder);
            newAccessories.transform.localPosition = Vector3.zero;
            newAccessories.transform.localRotation = Quaternion.identity;
        }

        private void DeleteAllAccessories()
        {
            for (int i = 0; i < _accessoriesHolder.childCount; i++)
            {
                Destroy(_accessoriesHolder.GetChild(0).gameObject);
            }
        }

        private void ColorButtonsInit()
        {
            foreach (var colorChooseButton in _colorChooseButtons)
            {
                colorChooseButton.Button.onClick.AddListener(() => { ChangeColor(colorChooseButton.Color); });
            }
        }

        private void AccessoriesButtonsInit()
        {
            foreach (var accessoriesChooseButton in _accessoriesChooseButtons)
            {
                accessoriesChooseButton.Button.onClick.AddListener(() =>
                {
                    ChangeAccessories(accessoriesChooseButton.Type);
                });
            }
        }

        public void SaveChanges()
        {
            _currentColor = _temporaryColor;
            _currentAccessoriesType = _temporaryAccessoriesType;

            _progressService.PlayerData.CustomCarData.CarColor = _currentColor;
            _progressService.PlayerData.CustomCarData.AccessoriesType = _currentAccessoriesType;

            _saveLoadService.SaveProgress();
            _roomMediator.OpenChangesAreSavedPopup();
        }

        private void SetActiveAvailableAccessoryButtons()
        {
            foreach (var accessoriesChooseButton in _accessoriesChooseButtons)
            {
                if (_progressService.PlayerData.CustomCarData.AvailableAccessories.Contains(
                        accessoriesChooseButton.Type))
                {
                    accessoriesChooseButton.gameObject.SetActive(true);
                }
                else
                {
                    accessoriesChooseButton.gameObject.SetActive(false);
                }
            }
        }

        private void SetActiveAvailableColorButtons()
        {
            foreach (var colorChooseButton in _colorChooseButtons)
            {
                if (_progressService.PlayerData.CustomCarData.AvailableColors.Contains(colorChooseButton.Color))
                {
                    colorChooseButton.gameObject.SetActive(true);
                }
                else
                {
                    colorChooseButton.gameObject.SetActive(false);
                }
            }
        }
    }
}