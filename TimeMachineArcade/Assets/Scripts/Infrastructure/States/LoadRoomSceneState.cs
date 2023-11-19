using Infrastructure.Logic;

namespace Infrastructure.States
{
    public class LoadRoomSceneState:IState
    {
        private const string MainMenu = "MainRoom";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadRoomSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }
        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<GameLoopState>();

        }
        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(MainMenu,OnLoaded);
            
        }
    }
}