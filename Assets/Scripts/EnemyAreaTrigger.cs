using UnityEngine;
using System.Collections.Generic;

public class EnemyAreaTrigger : MonoBehaviour
{
    public AudioSource backgroundMusic;     // Ссылка на компонент с фоновой музыкой
    public AudioSource victoryAudioSource;  // Ссылка на AudioSource для победного звука
    public AudioClip victorySound;          // Звук победы

    private bool hasPlayedVictorySound = false;
    private List<GameObject> enemiesInArea = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInArea.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInArea.Remove(other.gameObject);
            CheckEnemiesAndPlaySound();
        }
    }

    void Update()
    {
        CheckEnemiesAndPlaySound();
    }

    private void CheckEnemiesAndPlaySound()
    {
        enemiesInArea.RemoveAll(enemy => enemy == null);

        if (!hasPlayedVictorySound && enemiesInArea.Count == 0)
        {
            backgroundMusic.Stop(); // Останавливаем фоновую музыку

            if (victoryAudioSource != null && victorySound != null)
            {
                victoryAudioSource.clip = victorySound;
                victoryAudioSource.Play(); // Проигрываем победный звук
            }

            hasPlayedVictorySound = true;
        }
    }
}
