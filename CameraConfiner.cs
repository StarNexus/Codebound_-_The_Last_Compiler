using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineConfiner))]
public class CameraConfiner : MonoBehaviour
{
    [SerializeField] private Collider2D confinerCollider;

    private CinemachineConfiner cinemachineConfiner;

    private void Awake()
    {
        cinemachineConfiner = GetComponent<CinemachineConfiner>();
        if (confinerCollider != null)
        {
            cinemachineConfiner.m_BoundingShape2D = confinerCollider;
        }
        else
        {
            Debug.LogWarning("Confiner Collider is not assigned. Please assign a Collider2D.");
        }
    }

    private void OnValidate()
    {
        if (cinemachineConfiner == null)
        {
            cinemachineConfiner = GetComponent<CinemachineConfiner>();
        }

        if (confinerCollider != null)
        {
            cinemachineConfiner.m_BoundingShape2D = confinerCollider;
        }
    }
}
