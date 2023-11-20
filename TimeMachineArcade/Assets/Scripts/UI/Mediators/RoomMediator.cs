using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace UI
{
    public class RoomMediator : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _roomPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private GameObject _customizationPanel;
        [SerializeField]private CarCustomizationView _carCustomizationView;

        private PanelsSwitch _panelsSwitch;
        private GameStateMachine _gameStateMachine;
        private RoomMediator _roomMediator;
        private Popup _popup;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine,Popup popup)
        {
            _gameStateMachine = gameStateMachine;
            _popup = popup;
        }
        private void Start()
        {
            _panelsSwitch=new PanelsSwitch (_mainMenu);
        }

        public void ResetCarCustomization() => _carCustomizationView.Reset();
        public void OpenSingleLevel() => _gameStateMachine.Enter<LoadLevelSceneState>();

        public void OpenPreviousPanel() => _panelsSwitch.Back();
        public void OpenRoomPanel() => _panelsSwitch.OpenPanel(_roomPanel);
        public void OpenSettingsPanel() => _panelsSwitch.OpenPanel(_settingsPanel);
        public void OpenShopPanel() => _panelsSwitch.OpenPanel(_shopPanel);
        public void OpenNotEnoughMoneyPopup() => _popup.OpenPopup("Not Enough Money");
        public void OpenChangesAreSavedPopup() => _popup.OpenPopup("Changes Are Saved");
        public void OpenCustomizationPanel()
        {
            _panelsSwitch.OpenPanel(_customizationPanel);
            _carCustomizationView.UpdateButtons();
        }

    }
}
