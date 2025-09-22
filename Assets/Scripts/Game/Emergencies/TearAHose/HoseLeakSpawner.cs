using UnityEngine;

public class HoseLeakSpawner : MonoBehaviour
{
    [Header("Имя объекта шланга")]
    [SerializeField] private string hoseObjectName = "FuelHose"; // имя шланга в сцене

    [Header("Префаб партиклов утечки")]
    [SerializeField] private ParticleSystem leakPrefab;

    [Header("Смещение и поворот относительно шланга")]
    [SerializeField] private Vector3 localOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 localEulerAngles = new Vector3(0f, 0f, 0f);

    private ParticleSystem spawnedLeak;

    public void SpawnLeak()
    {
        if (spawnedLeak != null) return; // уже создано

        GameObject hose = GameObject.Find(hoseObjectName);
        if (hose == null)
        {
            Debug.LogWarning("Шланг с именем " + hoseObjectName + " не найден!");
            return;
        }

        if (leakPrefab == null)
        {
            Debug.LogWarning("Не назначен префаб партиклов!");
            return;
        }

        // Создаём партиклы как дочерний объект шланга
        spawnedLeak = Instantiate(leakPrefab, hose.transform);

        // Настраиваем локальную позицию и поворот
        spawnedLeak.transform.localPosition = localOffset;
        spawnedLeak.transform.localRotation = Quaternion.Euler(localEulerAngles);

        spawnedLeak.Play();
    }

    public void StopLeak()
    {
        if (spawnedLeak != null)
        {
            spawnedLeak.Stop();
            Destroy(spawnedLeak.gameObject, 1f);
            spawnedLeak = null;
        }
    }
}
