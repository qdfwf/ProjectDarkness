using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    private AudioSource audioSource; // Звук триггера
    private bool hasTriggered = false; // Флаг для отслеживания, сработал ли триггер
    public GameObject ambientSoundObject; // Объект с эмбиентом

    private AudioSource ambientAudioSource; // Ссылка на AudioSource эмбиента

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (ambientSoundObject != null)
        {
            ambientAudioSource = ambientSoundObject.GetComponent<AudioSource>(); // Получаем компонент AudioSource из объекта эмбиента
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, чтобы триггер срабатывал только один раз
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Устанавливаем флаг, чтобы звук больше не проигрывался

            // Останавливаем эмбиент
            if (ambientAudioSource != null && ambientAudioSource.isPlaying)
            {
                ambientAudioSource.Stop();
                Debug.Log("Ambient sound stopped.");
            }

            // Проигрываем звук триггера
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("Sound played on trigger once.");
            }
        }
    }
}
