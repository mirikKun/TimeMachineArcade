using Cinemachine;
using UnityEngine;

public class CameraTargeter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public void SetupTarget(Transform player)
    {
        _virtualCamera.Follow = player;
        _virtualCamera.LookAt = player;
    }
}
