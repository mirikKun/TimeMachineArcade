using UI.Mediators;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Panels
{
    public class UiGameEndPanel : MonoBehaviour
    {
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
            _restartButton.onClick.AddListener(_mediator.Restart);
            _toRoomButton.onClick.AddListener(_mediator.GoToRoomScene);
        
        }
    }
}
