using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;  // Ссылка на компонент Slider

    // Устанавливаем максимальное здоровье и обновляем UI
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;  // Устанавливаем максимальное значение
        slider.value = health;     // Устанавливаем текущее значение
    }

    // Обновляем текущее здоровье на полосе
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
