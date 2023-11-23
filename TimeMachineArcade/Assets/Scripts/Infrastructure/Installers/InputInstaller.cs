using Logic;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private SteerInput _steerInput;
        [SerializeField] private CoinsAnimator _coinsAnimator;
        public override void InstallBindings()
        {
            BindInput();
            BindCoinsAnimator();
        }

        private void BindInput()
        {
            Container
                .Bind<SteerInput>()
                .FromInstance(_steerInput)
                .AsSingle();
        }
        private void BindCoinsAnimator()
        {
            Container
                .Bind<CoinsAnimator>()
                .FromInstance(_coinsAnimator)
                .AsSingle();
        }
    }
}
