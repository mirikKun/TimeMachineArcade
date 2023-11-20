using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UiMainMenu : MonoBehaviour
    {
        [SerializeField] private Button _openRoomButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _startSingleButton;
        [SerializeField] private Button _openShopButton;
        [SerializeField] private Button _settingsShopButton;

        private RoomMediator _mediator;

        [Inject]
        private void Construct(RoomMediator mediator)
        {
            _mediator = mediator;
        }

        private void Start()
        {
            _startSingleButton.onClick.AddListener(_mediator.OpenSingleLevel);
            _openRoomButton.onClick.AddListener(_mediator.OpenRoomPanel);
            _openShopButton.onClick.AddListener(_mediator.OpenShopPanel);
            _settingsShopButton.onClick.AddListener(_mediator.OpenSettingsPanel);

            _exitButton.onClick.AddListener(Exit);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}