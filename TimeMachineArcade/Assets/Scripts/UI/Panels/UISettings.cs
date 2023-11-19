using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    private RoomMediator _mediator;

    [Inject]
    private void Construct(RoomMediator mediator)
    {
        _mediator = mediator;
    }

    private void Start()
    {
        _backButton.onClick.AddListener(_mediator.OpenPreviousPanel);
    }
}