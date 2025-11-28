using Unity.VisualScripting;
using UnityEngine;

public class EmergencyTrigger : MonoBehaviour
{
    [SerializeField] private CarMovement car;       // ссылка на машину
    [SerializeField] private GameObject _fuelTank;
    [SerializeField] private Transform _fuelPistol; // сам пистолет
    [SerializeField] private ParticleSystem leakPS; // партиклы утечки
    [SerializeField] private CreateFuelPistol _fuelPump;
    private HoseController _hoseEndPoint;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            // Прикрепляем пистолет к машине. Машина начинает движение
            AttachFuelPistolToCar();
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

    private void AttachFuelPistolToCar()
    {
        foreach (Transform fuelPistolAnchor in _fuelPump.fuelPistolAnchors)
        {
            if (fuelPistolAnchor.name == "FuelPistolAnchor3")
            {
                HoseController hosePoint = GameObject.Find("Hose3").GetComponent<HoseController>();
                hosePoint.hoseEndPoint = null;
                hosePoint.hoseStartPoint = null;
                Debug.Log("Отсоединили шланг от пистолета");

                foreach (GameObject fuelPistol in _fuelPump.fuelPistols)
                {
                    if (fuelPistol.name == "FuelPistol3")
                    {
                        _fuelPistol = fuelPistol.transform;
                        _fuelPistol.SetParent(_fuelTank.transform);
                        _fuelPistol.transform.localPosition = Vector3.zero;
                        _fuelPistol.transform.localRotation = Quaternion.identity;
                        
                        Debug.Log("Пистолет прикреплён к машине через EmergencyTrigger");
                        //foreach (GameObject hose in fuelPistol.transform)
                        //{
                        //    if (hose.name == "HoseEndPoint")
                        //    {
                                
                                
                        //    }
                        //}
                    }
                }
            }
        }
    }
}
