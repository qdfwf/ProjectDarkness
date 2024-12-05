using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;       // Максимальное здоровье игрока
    public int currentHealth;         // Текущее здоровье игрока
    public Image healthBarFill;       // Ссылка на UI-элемент полоски здоровья

    void Start()
    {
        currentHealth = maxHealth;    // Устанавливаем текущее здоровье на максимум
        UpdateHealthUI();             // Инициализируем полоску здоровья
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;      // Уменьшаем здоровье
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ограничиваем в пределах

        UpdateHealthUI();             // Обновляем UI

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth; // Вычисляем заполнение
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // Логика смерти игрока
    }
}
