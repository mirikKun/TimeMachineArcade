using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiPausePanel : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _toRoomButton;
    private GameSingleLevelMediator _mediator;
    
    [Inject]
    private void Construct(GameSingleLevelMediator mediator)
    {
        _mediator = mediator;
    }
    private void Start()
    {
        _continueButton.onClick.AddListener(_mediator.OpenPreviousPanel);
        _toRoomButton.onClick.AddListener(_mediator.GoToRoomScene);
        _restartButton.onClick.AddListener(_mediator.Restart);
        
    }
}
