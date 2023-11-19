using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResetPrefs : MonoBehaviour
{
    [SerializeField] private Button _button;
    private GameStateMachine _stateMachine;

    [Inject]
    private void Construct(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    private void Start()
    {
        _button.onClick.AddListener(ClearPrefs);
    }

    
    private void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();        
        _stateMachine.Enter<BootstrapState>();
    }
}
