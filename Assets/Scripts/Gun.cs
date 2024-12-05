using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;              // Урон, который наносит пуля
    public float range = 100f;              // Дальность стрельбы
    public float fireRate = 15f;            // Скорость стрельбы (выстрелов в секунду)
    public Camera fpsCamera;                // Камера игрока
    public ParticleSystem muzzleFlash;      // Частицы для вспышки выстрела
    public GameObject impactEffect;         // Эффект при попадании пули
    public AudioSource gunSound;            // Источник звука для выстрела
    public LineRenderer bulletTracer;       // Трассер пули
    public Light muzzleLight;               // Вспышка света при выстреле (необязательно)

    private float nextTimeToFire = 0f;      // Время до следующего выстрела
    private bool isFiring = false;          // Отслеживание состояния стрельбы

    void Update()
    {
        // Проверяем нажатие кнопки огня и таймер выстрела
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (!isFiring)
            {
                StartFiring(); // Запускаем звук, если ещё не стреляем
            }

            nextTimeToFire = Time.time + 1f / fireRate;  // Рассчитываем задержку до следующего выстрела
            Shoot();                                    // Вызываем функцию стрельбы
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopFiring(); // Останавливаем звук при отпускании кнопки стрельбы
        }
    }

    void Shoot()
    {
        // Воспроизведение вспышки
        muzzleFlash.Play();

        // Включение света при выстреле
        if (muzzleLight != null)
        {
            muzzleLight.enabled = true;
            Invoke("TurnOffMuzzleLight", 0.05f);  // Отключаем свет через 0.05 секунд
        }

        // Проверяем попадание при помощи Raycast
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);  // Выводим имя объекта, в который попала пуля

            // Проверяем, есть ли у объекта скрипт Target, и наносим урон
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);  // Наносим урон
            }

            // Создаем эффект удара на месте попадания пули
            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);  // Удаляем эффект через 2 секунды
            }

            // Визуализация трассера пули
            StartCoroutine(DrawBulletTracer(hit.point));
        }
    }

    // Корутин для трассера пули
    IEnumerator DrawBulletTracer(Vector3 hitPoint)
    {
        // Установить начальную и конечную точки трассера
        bulletTracer.SetPosition(0, fpsCamera.transform.position);  // Начальная точка — камера игрока
        bulletTracer.SetPosition(1, hitPoint);  // Конечная точка — место попадания

        // Включаем трассер на короткое время
        bulletTracer.enabled = true;
        yield return new WaitForSeconds(0.05f);  // Трассер будет виден 0.05 секунды
        bulletTracer.enabled = false;  // Отключаем трассер
    }

    // Функция для включения звука и зацикливания
    void StartFiring()
    {
        isFiring = true;
        gunSound.loop = true;  // Включаем зацикливание
        gunSound.Play();       // Проигрываем звук
    }

    // Функция для остановки звука
    void StopFiring()
    {
        isFiring = false;
        gunSound.loop = false;  // Отключаем зацикливание
        gunSound.Stop();        // Останавливаем звук
    }

    // Функция для отключения света вспышки
    void TurnOffMuzzleLight()
    {
        if (muzzleLight != null)
        {
            muzzleLight.enabled = false;
        }
    }
}
