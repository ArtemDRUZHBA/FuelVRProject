using UnityEngine;

public class HoseLeakSpawner : MonoBehaviour
{
    [Header("Имя объекта шланга")]
    [SerializeField] private string hoseObjectName = "Hose7"; // имя шланга в сцене

    [Header("Имя точки конца шланга в префабе")]
    [SerializeField] private string hoseEndName = "HoseEnd";

    [Header("Префаб партиклов утечки")]
    [SerializeField] private ParticleSystem fuelPSPrefab;


    private ParticleSystem spawnedPS;

    public void SpawnLeak()
    {
        print("Метод SpawnLeak запущен");
        if (spawnedPS != null) return; // уже создано
        print("Метод SpawnLeak работает дальше");

        // Ищем шланг
        var hose = GameObject.Find(hoseObjectName);
        if (hose == null) return;

        // Находим HoseEnd
        var hoseEnd = hose.transform.FindDeepChild(hoseEndName);
        if (hoseEnd == null) return;

        // Спавним готовый префаб прямо в HoseEnd
        spawnedPS = Instantiate(fuelPSPrefab, hoseEnd.position, hoseEnd.rotation, hoseEnd);

        // Запускаем партиклы
        var ps = spawnedPS.GetComponent<ParticleSystem>();
        if (ps != null) ps.Play();
    }

    public void StopLeak()
    {
        if (spawnedPS != null)
        {
            var ps = spawnedPS.GetComponent<ParticleSystem>();
            if (ps != null) ps.Stop();
            //Destroy(spawnedPS, 1f);
            //spawnedPS = null;
        }
    }
}
