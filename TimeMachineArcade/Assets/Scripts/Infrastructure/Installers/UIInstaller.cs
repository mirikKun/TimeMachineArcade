using Logic;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private SteerInput _steerInput;
        [SerializeField] private CoinsAnimator _coinsAnimator;
        [SerializeField] private TransitionEffect _transitionEffect;
        public override void InstallBindings()
        {
            BindInput();
            BindCoinsAnimator();
            BindTransition();
        }

        private void BindInput()
        {
            Container
                .Bind<SteerInput>()
                .FromInstance(_steerInput)
                .AsSingle();
        }      
        private void BindTransition()
        {
            Container
                .Bind<TransitionEffect>()
                .FromInstance(_transitionEffect)
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
