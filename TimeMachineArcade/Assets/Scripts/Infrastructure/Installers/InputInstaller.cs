using Logic;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private SteerInput _steerInput;
        public override void InstallBindings()
        {
            Container
                .Bind<SteerInput>()
                .FromInstance(_steerInput)
                .AsSingle();
      
        }
    }
}
