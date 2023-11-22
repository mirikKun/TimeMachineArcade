using Infrastructure.States;
using Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Mediators
{
    public class GameSingleLevelMediator:MonoBehaviour,IGameMediator
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _gameEndPanel;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Game _game;

        private PanelsSwitch _panelsSwitch;
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        private void Start()
        {
            _panelsSwitch = new PanelsSwitch(_pauseButton.gameObject);
            _pauseButton.onClick.AddListener(OpenPauseMenu);
        }

        public void OpenPauseMenu() => _panelsSwitch.OpenPanel(_pausePanel);
        public void OpenGameEndPanel() => _panelsSwitch.OpenPanel(_gameEndPanel);
        public void GoToRoomScene() => _stateMachine.Enter<LoadHubSceneState>();
        public void OpenPreviousPanel() => _panelsSwitch.Back();
        public void Restart()
        {
            _game.ResetGame();
            OpenPreviousPanel();
        }
    }
}