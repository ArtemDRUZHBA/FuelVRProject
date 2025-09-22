using UnityEngine;

public class EmergencyTrigger : MonoBehaviour
{
    [SerializeField] private CarMovement car;       // ссылка на машину
    [SerializeField] private Transform nozzle; // сам пистолет
    [SerializeField] private ParticleSystem leakPS; // партиклы утечки

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            // Прикрепляем пистолет к машине. Машина начинает движение
            AttachNozzleToCar();
            car.StopFueling();

            // Запускаем утечку через HoseLeakSpawner
            var spawner = GetComponent<HoseLeakSpawner>();
            if (spawner != null)
            {
                spawner.SpawnLeak();
            }
            else Debug.LogWarning("Spawner Null метод не запущен!");

            FindObjectOfType<TaskUIControllerFromFile>().CompleteTask();

        }
    }

    private void AttachNozzleToCar()
    {
        if (nozzle != null && car != null)
        {
            nozzle.SetParent(car.transform);
            Debug.Log("Пистолет прикреплён к машине через EmergencyTrigger");
        }
    }
}
