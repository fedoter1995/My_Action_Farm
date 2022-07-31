using Cinemachine;
using UnityEngine;
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void Initialize(Transform obj)
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

        virtualCamera.LookAt = obj;
        virtualCamera.Follow = obj;
    }
}
