using Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using UI.Mediators;
using UnityEngine;
using Zenject;

namespace UI.Shop
{
    public class UiShop : MonoBehaviour
    {
        [SerializeField] private ColorBuyButton[] _colorBuyButtons;
        [SerializeField] private AccessoryBuyButton[] _accessoriesBuyButtons;

        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _progressService;
        private RoomMediator _mediator;

        [Inject]
        private void Construct(RoomMediator mediator, ISaveLoadService saveLoadService,
            IPersistentProgressService progressService)
        {
            _mediator = mediator;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        private void Start()
        {
            ColorButtonsInit();
            AccessoriesButtonsInit();
        }

        private void OnEnable()
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            if (_progressService.PlayerData == null)
                return;
            SetActiveAvailableColorButtons();
            SetActiveAvailableAccessoryButtons();
        }

        private void AccessoriesButtonsInit()
        {
            foreach (var accessoriesBuyButton in _accessoriesBuyButtons)
            {
                accessoriesBuyButton.Button.onClick.AddListener(() =>
                {
                    TryBuyAccessory(accessoriesBuyButton.Type, accessoriesBuyButton.Price);
                });
            }
        }

        private void TryBuyAccessory(AccessoriesType type, int price)
        {
            if (_progressService.PlayerData.MoneyData.CanBuy(price))
            {
                _progressService.PlayerData.CustomCarData.AvailableAccessories.Add(type);
                BuyItem(price);
            }
            else
            {
                _mediator.OpenNotEnoughMoneyPopup();
            }
        }


        private void ColorButtonsInit()
        {
            foreach (var colorBuyButton in _colorBuyButtons)
            {
                colorBuyButton.Button.onClick.AddListener(() =>
                {
                    TryBuyColor(colorBuyButton.Color, colorBuyButton.Price);
                });
            }
        }

        private void TryBuyColor(ColorType type, int price)
        {
            if (_progressService.PlayerData.MoneyData.CanBuy(price))
            {
                _progressService.PlayerData.CustomCarData.AvailableColors.Add(type);
                BuyItem(price);
            }
            else
            {
                _mediator.OpenNotEnoughMoneyPopup();
            }
        }

        private void BuyItem(int price)
        {
            _progressService.PlayerData.MoneyData.Spend(price);
            _saveLoadService.SaveProgress();
            UpdateButtons();
        }

        private void SetActiveAvailableAccessoryButtons()
        {
            foreach (var accessoriesBuyButton in _accessoriesBuyButtons)
            {
                if (!_progressService.PlayerData.CustomCarData.AvailableAccessories.Contains(accessoriesBuyButton.Type))
                {
                    accessoriesBuyButton.gameObject.SetActive(true);
                }
                else
                {
                    accessoriesBuyButton.gameObject.SetActive(false);
                }
            }
        }

        private void SetActiveAvailableColorButtons()
        {
            foreach (var colorBuyButton in _colorBuyButtons)
            {
                if (!_progressService.PlayerData.CustomCarData.AvailableColors.Contains(colorBuyButton.Color))
                {
                    colorBuyButton.gameObject.SetActive(true);
                }
                else
                {
                    colorBuyButton.gameObject.SetActive(false);
                }
            }
        }
    }
}