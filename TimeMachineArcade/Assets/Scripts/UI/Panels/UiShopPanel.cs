using UI.Mediators;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Panels
{
    public class UiShopPanel:MonoBehaviour
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
            _backButton.onClick.AddListener((() =>
            {
                _mediator.OpenPreviousPanel();
                _mediator.ResetCarCustomization();
            }));

        
     
        }
    }
}