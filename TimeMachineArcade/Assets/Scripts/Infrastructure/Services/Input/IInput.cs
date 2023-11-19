using UnityEngine;

namespace DefaultNamespace
{
    public interface IInput
    {
        public float RotateInput { get; set; }
        public float GasInput { get; set; }
  
        void UpdateRotation(float rotation);
        void UpdateSpeed( float speed);
    }
}