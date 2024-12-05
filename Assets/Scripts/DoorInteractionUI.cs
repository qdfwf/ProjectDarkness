using UnityEngine;
using TMPro;

public class DoorInteractionUI : MonoBehaviour
{
    public TextMeshProUGUI interactionText; // Ссылка на UI-элемент
    public float activationDistance = 2f;  // Дистанция, на которой появляется текст
    private Transform player;              // Ссылка на игрока

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        interactionText.gameObject.SetActive(false); // Скрываем текст при старте
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Если игрок в зоне взаимодействия, показываем текст
        if (distance <= activationDistance)
        {
            interactionText.gameObject.SetActive(true);

            // Опционально: можно добавить вращение текста за игроком
            
        }
        else
        {
            interactionText.gameObject.SetActive(false); // Скрываем текст
        }
    }
}
