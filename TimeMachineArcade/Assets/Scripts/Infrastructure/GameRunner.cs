using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField]private GameBootstrapper _bootstrapperPrefab;
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {

            _stateMachine = stateMachine;
        }
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
      
            if(bootstrapper != null) return;

            GameBootstrapper gameBootstrapper = Instantiate(_bootstrapperPrefab);
            gameBootstrapper.Setup(_stateMachine);
        }
    }
}