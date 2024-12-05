using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Угол, на который дверь откроется
    public float openSpeed = 2f;  // Скорость открытия
    public KeyCode interactKey = KeyCode.F; // Клавиша для взаимодействия
    public AudioSource doorAudio; // Ссылка на компонент AudioSource с музыкой

    private bool isOpen = false; // Состояние двери
    private bool hasPlayedMusic = false; // Флаг для отслеживания, была ли уже запущена музыка
    private Quaternion closedRotation; // Закрытое положение двери
    private Quaternion openRotation; // Открытое положение двери

    private Transform playerTransform; // Ссылка на трансформ игрока
    private float interactionDistance = 2f; // Максимальное расстояние для взаимодействия

    void Start()
    {
        // Сохраняем исходное и целевое положение двери
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;

        // Находим игрока по тегу
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // Проверяем, находится ли игрок в радиусе взаимодействия
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(interactKey))
            {
                isOpen = !isOpen; // Меняем состояние двери
                
                // Запуск музыки при первом открытии двери
                if (isOpen && !hasPlayedMusic && doorAudio != null)
                {
                    doorAudio.Play();
                    hasPlayedMusic = true; // Обновляем флаг, чтобы музыка не играла повторно
                }
            }
        }

        // Интерполяция между закрытым и открытым положением
        transform.rotation = Quaternion.Slerp(transform.rotation, isOpen ? openRotation : closedRotation, Time.deltaTime * openSpeed);
    }
}
