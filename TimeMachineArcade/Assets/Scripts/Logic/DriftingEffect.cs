using UnityEngine;

namespace Logic
{
    public class DriftingEffect:MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private float _rate=3;
        private float _time;

        private bool _ready=true;

        private void Update()
        {
            if(!_ready)
            {
                _time += Time.deltaTime * _rate;
                if (_time > 1)
                {
                    _ready = true;
                    _time = 0;
                }
            }
        }

        public void EnableEffect()
        {
            if (_ready)
            {
                _particleSystem.Play();
                _ready = false;
            }
        }
    }
}