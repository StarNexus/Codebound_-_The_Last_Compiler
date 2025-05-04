using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineConfiner2D))]
public class CameraConfiner : MonoBehaviour
{
    [SerializeField] private Collider2D confinerCollider;
    [SerializeField] private float orthographicSize = 5f; // Controls how much of the room is visible

    private CinemachineConfiner2D cinemachineConfiner;
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        cinemachineConfiner = GetComponent<CinemachineConfiner2D>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (confinerCollider != null)
        {
            cinemachineConfiner.m_BoundingShape2D = confinerCollider;
            cinemachineConfiner.m_Damping = 0.5f; // Add smooth dampening to camera movement
            cinemachineConfiner.m_MaxWindowSize = 0.5f; // Adjust as needed for your room size
        }
        else
        {
            Debug.LogWarning("Confiner Collider is not assigned. Please assign a Collider2D.");
        }

        if (virtualCamera != null)
        {
            // Configure the virtual camera for the boss room
            virtualCamera.m_Lens.OrthographicSize = orthographicSize;
            
            // Set up the Body properties
            var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (framingTransposer != null)
            {
                framingTransposer.m_DeadZoneWidth = 0f; // No horizontal dead zone
                framingTransposer.m_DeadZoneHeight = 0f; // No vertical dead zone
                framingTransposer.m_CameraDistance = 10f; // Adjust based on your needs
                framingTransposer.m_UnlimitedSoftZone = true; // Allow camera to follow target freely within bounds
            }
        }
    }

    private void OnValidate()
    {
        if (cinemachineConfiner == null)
        {
            cinemachineConfiner = GetComponent<CinemachineConfiner2D>();
        }

        if (confinerCollider != null)
        {
            cinemachineConfiner.m_BoundingShape2D = confinerCollider;
        }
    }

    public void SetConfiner(Collider2D newConfiner)
    {
        confinerCollider = newConfiner;
        if (cinemachineConfiner != null)
        {
            cinemachineConfiner.m_BoundingShape2D = confinerCollider;
            cinemachineConfiner.InvalidateCache(); // Important when changing confiner at runtime
        }
    }
}
