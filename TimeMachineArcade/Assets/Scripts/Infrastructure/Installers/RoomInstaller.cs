using UI;
using UI.Mediators;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class RoomInstaller : MonoInstaller
    {
        [SerializeField] private RoomMediator _roomMediator;
        [SerializeField] private Popup _popup;
        public override void InstallBindings()
        {
            Container.Bind<RoomMediator>().FromInstance(_roomMediator);
            Container.Bind<Popup>().FromInstance(_popup);
        }
    }
}
