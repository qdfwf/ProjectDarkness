using UnityEngine;

public class AC130Control : MonoBehaviour
{
    public Camera mainCamera; // Основная камера
    public Camera ac130Camera; // Камера AC-130
    public GameObject player; // Игрок (его объект)
    public Transform rotationCenter; // Точка, вокруг которой летает самолет
    public float flightRadius = 50f; // Радиус полета
    public float heightOffset = 30f; // Высота над игроком
    public float rotationSpeed = 30f; // Скорость вращения самолета
    public float cameraSensitivity = 2f; // Чувствительность камеры

    public AudioSource gunAudioSource1; // AudioSource для оружия 1
    public AudioSource gunAudioSource2; // AudioSource для оружия 2
    public AudioSource gunAudioSource3; // AudioSource для оружия 3

    private AudioSource currentAudioSource; // Текущий источник звука
    private bool isFiring = false; // Флаг стрельбы
    private bool isAC130Active = false; // Флаг активации AC-130
    private int currentWeapon = 1; // Текущее оружие: 1, 2 или 3
    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    private GameObject crosshair; // Перекрестие

    void Start()
    {
        if (ac130Camera != null)
        {
            ac130Camera.enabled = false; // Камера AC-130 выключена по умолчанию
        }

        // Устанавливаем начальный AudioSource
        currentAudioSource = gunAudioSource1;

        // Находим объект перекрестия
        CrosshairControl crosshairControl = FindObjectOfType<CrosshairControl>();
        if (crosshairControl != null)
        {
            crosshair = crosshairControl.crosshair;
            if (crosshair != null)
            {
                crosshair.SetActive(false); // Отключаем перекрестие по умолчанию
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5)) // Нажатие клавиши "5"
        {
            ToggleAC130();
        }

        if (isAC130Active)
        {
            RotateAC130();
            ControlCamera();

            // Переключение оружия на 1, 2 или 3
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

            // Стрельба самолета только при нажатии ЛКМ
            if (Input.GetMouseButton(0)) // 0 - левая кнопка мыши
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

        // Включаем или отключаем перекрестие
        if (crosshair != null)
        {
            crosshair.SetActive(isAC130Active);
        }

        // Активируем или деактивируем игрока
        if (player != null)
        {
            player.SetActive(!isAC130Active);
        }

        if (isAC130Active)
        {
            // Устанавливаем начальную позицию самолета
            rotationCenter.position = player.transform.position + Vector3.up * heightOffset;
            Vector3 offset = new Vector3(flightRadius, 0, 0);
            transform.position = rotationCenter.position + offset;

            // Сбрасываем вращение камеры
            horizontalRotation = 0f;
            verticalRotation = 0f;
        }
    }

    void RotateAC130()
    {
        // Вращаем самолет по кругу вокруг игрока
        transform.RotateAround(rotationCenter.position, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.LookAt(rotationCenter.position);
    }

    void ControlCamera()
    {
        // Управление углами поворота камеры с помощью мышки
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); // Ограничиваем вертикальный угол

        ac130Camera.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    void SwitchWeapon(int weaponNumber)
    {
        if (weaponNumber == currentWeapon) return; // Если выбран тот же тип оружия, ничего не делаем

        currentWeapon = weaponNumber;

        // Сообщение при переключении оружия
        Debug.Log("Switched to weapon " + currentWeapon);

        // Переключаем AudioSource
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

        // Останавливаем стрельбу текущего оружия
        StopFiring();
    }

    void FireWeapon()
    {
        if (!isFiring)
        {
            isFiring = true;
            currentAudioSource?.Play(); // Проигрываем звук, если он установлен
        }
    }

    void StopFiring()
    {
        if (isFiring)
        {
            isFiring = false;
            currentAudioSource?.Stop(); // Останавливаем звук
        }
    }
}
