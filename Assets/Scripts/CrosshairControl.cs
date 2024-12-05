using UnityEngine;

public class CrosshairControl : MonoBehaviour
{
    public GameObject crosshair; // —сылка на объект перекрести€

    public void ToggleCrosshair(bool isVisible)
    {
        if (crosshair != null)
        {
            crosshair.SetActive(isVisible);
        }
    }
}
