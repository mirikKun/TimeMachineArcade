using UI.Mediators;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Panels
{
    public class UIRoomPanel : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _recordsButton;
        [SerializeField] private Button _customizationsButton;
    
        private RoomMediator _mediator;
        [Inject]
        private void Construct(RoomMediator mediator)
        {
            _mediator = mediator;
        }

        private void Start()
        {
            _backButton.onClick.AddListener(_mediator.OpenPreviousPanel);
            _customizationsButton.onClick.AddListener(_mediator.OpenCustomizationPanel);
        
        }
    
    

    }
}
