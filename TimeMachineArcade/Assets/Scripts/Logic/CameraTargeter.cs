using UnityEngine;
using Zenject;

namespace Logic
{
    public class CameraTargeter : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _cameraZOffset;
        private Transform _player;


        [Inject]
        public void Construct(CarMover player)
        {
            _player = player.transform;
        }

        private void Update()
        {
            SetCameraPosition();
        }

        private void SetCameraPosition()
        {
            _camera.position = new Vector3(_camera.position.x, _camera.position.y, _player.position.z + _cameraZOffset);
        }
    }
}
