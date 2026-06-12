using UnityEngine;

[DisallowMultipleComponent]
public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 2.25f, -15f);
    [SerializeField, Min(0f)] private float smoothTime = 0.35f;

    private float horizontalVelocity;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InstallOnMainCamera()
    {
        Camera mainCamera = Camera.main;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (mainCamera == null || player == null)
        {
            return;
        }

        CameraFollow2D follow = mainCamera.GetComponent<CameraFollow2D>();
        if (follow == null)
        {
            follow = mainCamera.gameObject.AddComponent<CameraFollow2D>();
        }

        follow.target = player.transform;
        follow.DisableCompetingCameraDrivers();
    }

    private void Awake()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        DisableCompetingCameraDrivers();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.SmoothDamp(
            cameraPosition.x,
            target.position.x + offset.x,
            ref horizontalVelocity,
            smoothTime);
        transform.position = cameraPosition;
    }

    private void DisableCompetingCameraDrivers()
    {
        foreach (MonoBehaviour behaviour in GetComponents<MonoBehaviour>())
        {
            if (behaviour == this)
            {
                continue;
            }

            string componentNamespace = behaviour.GetType().Namespace;
            if (!string.IsNullOrEmpty(componentNamespace) &&
                componentNamespace.StartsWith("Unity.Cinemachine"))
            {
                behaviour.enabled = false;
            }
        }
    }
}
