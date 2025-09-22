using UnityEngine;

public class EmergencyTrigger : MonoBehaviour
{
    [SerializeField] private CarMovement car;       // ссылка на машину
    [SerializeField] private TearHoseController hose;   // ссылка на шланг
    [SerializeField] private ParticleSystem leakPS; // партиклы утечки

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            // ћашина начинает движение
            car.StopFueling();

            // Ўланг отрываетс€
            if (hose != null)
                hose.DetachFromPump();

            // «апускаем утечку через HoseLeakSpawner
            var spawner = GetComponent<HoseLeakSpawner>();
            if (spawner != null)
                spawner.SpawnLeak();
        }
    }
}
