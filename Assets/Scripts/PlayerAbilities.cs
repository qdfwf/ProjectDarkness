using UnityEngine;

public class PlayerTeleportAndSlowdown : MonoBehaviour
{
    public Transform cameraTransform;
    public float teleportDistance = 10f;
    public float slowMotionScale = 0.2f;
    private float normalTimeScale = 1f;
    public string teleportableTag = "TeleportSurface"; // Имя тега для телепортируемых поверхностей

    void Update()
    {
        HandleTeleport();
        HandleSlowMotion();
    }

    void HandleTeleport()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            // Проверка попадания луча
            if (Physics.Raycast(ray, out hit, teleportDistance))
            {
                // Проверяем, имеет ли поверхность нужный тег
                if (hit.collider.CompareTag(teleportableTag))
                {
                    transform.position = hit.point;
                    Debug.Log("Teleported to: " + hit.point);
                }
                else
                {
                    Debug.Log("Surface is not teleportable.");
                }
            }
            else
            {
                Debug.Log("No surface detected for teleport.");
            }
        }
    }

    void HandleSlowMotion()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = slowMotionScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Debug.Log("Time slowed down");
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            Time.timeScale = normalTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Debug.Log("Time resumed");
        }
    }
}
