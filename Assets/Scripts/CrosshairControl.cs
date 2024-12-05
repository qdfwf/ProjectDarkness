using UnityEngine;

public class CrosshairControl : MonoBehaviour
{
    public GameObject crosshair; // ������ �� ������ �����������

    public void ToggleCrosshair(bool isVisible)
    {
        if (crosshair != null)
        {
            crosshair.SetActive(isVisible);
        }
    }
}
