using UnityEngine;

public class AC130Control : MonoBehaviour
{
    public Camera mainCamera; // �������� ������
    public Camera ac130Camera; // ������ AC-130
    public GameObject player; // ����� (��� ������)
    public Transform rotationCenter; // �����, ������ ������� ������ �������
    public float flightRadius = 50f; // ������ ������
    public float heightOffset = 30f; // ������ ��� �������
    public float rotationSpeed = 30f; // �������� �������� ��������
    public float cameraSensitivity = 2f; // ���������������� ������

    public AudioSource gunAudioSource1; // AudioSource ��� ������ 1
    public AudioSource gunAudioSource2; // AudioSource ��� ������ 2
    public AudioSource gunAudioSource3; // AudioSource ��� ������ 3

    private AudioSource currentAudioSource; // ������� �������� �����
    private bool isFiring = false; // ���� ��������
    private bool isAC130Active = false; // ���� ��������� AC-130
    private int currentWeapon = 1; // ������� ������: 1, 2 ��� 3
    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    private GameObject crosshair; // �����������

    void Start()
    {
        if (ac130Camera != null)
        {
            ac130Camera.enabled = false; // ������ AC-130 ��������� �� ���������
        }

        // ������������� ��������� AudioSource
        currentAudioSource = gunAudioSource1;

        // ������� ������ �����������
        CrosshairControl crosshairControl = FindObjectOfType<CrosshairControl>();
        if (crosshairControl != null)
        {
            crosshair = crosshairControl.crosshair;
            if (crosshair != null)
            {
                crosshair.SetActive(false); // ��������� ����������� �� ���������
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5)) // ������� ������� "5"
        {
            ToggleAC130();
        }

        if (isAC130Active)
        {
            RotateAC130();
            ControlCamera();

            // ������������ ������ �� 1, 2 ��� 3
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchWeapon(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWeapon(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchWeapon(3);
            }

            // �������� �������� ������ ��� ������� ���
            if (Input.GetMouseButton(0)) // 0 - ����� ������ ����
            {
                FireWeapon();
            }
            else
            {
                StopFiring();
            }
        }
    }

    void ToggleAC130()
    {
        isAC130Active = !isAC130Active;

        if (mainCamera != null) mainCamera.enabled = !isAC130Active;
        if (ac130Camera != null) ac130Camera.enabled = isAC130Active;

        // �������� ��� ��������� �����������
        if (crosshair != null)
        {
            crosshair.SetActive(isAC130Active);
        }

        // ���������� ��� ������������ ������
        if (player != null)
        {
            player.SetActive(!isAC130Active);
        }

        if (isAC130Active)
        {
            // ������������� ��������� ������� ��������
            rotationCenter.position = player.transform.position + Vector3.up * heightOffset;
            Vector3 offset = new Vector3(flightRadius, 0, 0);
            transform.position = rotationCenter.position + offset;

            // ���������� �������� ������
            horizontalRotation = 0f;
            verticalRotation = 0f;
        }
    }

    void RotateAC130()
    {
        // ������� ������� �� ����� ������ ������
        transform.RotateAround(rotationCenter.position, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.LookAt(rotationCenter.position);
    }

    void ControlCamera()
    {
        // ���������� ������ �������� ������ � ������� �����
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); // ������������ ������������ ����

        ac130Camera.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    void SwitchWeapon(int weaponNumber)
    {
        if (weaponNumber == currentWeapon) return; // ���� ������ ��� �� ��� ������, ������ �� ������

        currentWeapon = weaponNumber;

        // ��������� ��� ������������ ������
        Debug.Log("Switched to weapon " + currentWeapon);

        // ����������� AudioSource
        switch (currentWeapon)
        {
            case 1:
                currentAudioSource = gunAudioSource1;
                break;
            case 2:
                currentAudioSource = gunAudioSource2;
                break;
            case 3:
                currentAudioSource = gunAudioSource3;
                break;
        }

        // ������������� �������� �������� ������
        StopFiring();
    }

    void FireWeapon()
    {
        if (!isFiring)
        {
            isFiring = true;
            currentAudioSource?.Play(); // ����������� ����, ���� �� ����������
        }
    }

    void StopFiring()
    {
        if (isFiring)
        {
            isFiring = false;
            currentAudioSource?.Stop(); // ������������� ����
        }
    }
}
