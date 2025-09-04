using System.Collections;
using UnityEngine;

public class FuelSpotController : MonoBehaviour, IInteractable
{
    private bool occupied = false;
    private bool reserved = false;
    private GameObject exitPath;   // Индивидуальный путь выезда для этой колонки
    private GameObject finalPath;  // Общий финальный путь (один для всех)

    public FuelSpotController currentFuelStation;

    public GameObject fuelBarPrefab;

    private FuelBarController currentFuelBar;

    // Флаг активации заправки игроком
    private bool fuelingActivated = false;
    private bool isFuelingStarted = false;

    // Ссылка на заправляемую машину
    private Transform currentFuelingCar;

    public bool IsOccupied() => occupied;
    public bool IsReserved() => reserved;

    public void ReserveSpot()
    {
        reserved = true;
        Debug.Log($"Колонка {gameObject.name} зарезервирована.");
    }

    public void ReleaseReservation()
    {
        reserved = false;
    }

    // Установка индивидуальных путей: exitPath и общий finalPath
    public void SetPaths(GameObject exitPathObj, GameObject finalPathObj)
    {
        exitPath = exitPathObj;
        finalPath = finalPathObj;
    }

    // Пытаемся запустить заправку, если машина (с тегом "Car") входит в зону триггера
    private void TryStartFueling(Collider other)
    {
        if (!other.CompareTag("Car"))
            return;
        if (occupied || !reserved)
            return;

        CarMovement carMovement = other.GetComponent<CarMovement>();
        if (carMovement != null)
        {
            Debug.Log($"Колонка {gameObject.name}: обнаружена машина {other.gameObject.name} для заправки.");
            OccupySpot(other.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TryStartFueling(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryStartFueling(other);
    }

    public void OccupySpot(Transform car)
    {
        if (occupied)
        {
            Debug.LogWarning($"Колонка {gameObject.name} уже занята!");
            return;
        }
        occupied = true;
        ReleaseReservation();
        currentFuelingCar = car;

        // Запишем ссылку на себя в автомобиль
        CarMovement carMovement = car.GetComponent<CarMovement>();
        if (carMovement != null)
        {
            carMovement.currentFuelStation = this;
        }

        Debug.Log($"Машина {car.gameObject.name} начала заправку на колонке {gameObject.name}. Ожидание взаимодействия с игроком...");
        StartCoroutine(FuelRoutine(car));
    }


    // Этот метод вызывается скриптом игрока при попадании луча на авто, находящееся на колонке
    public void ActivateFueling()
    {
        if (isFuelingStarted) return; // Защита от повторного запуска
        isFuelingStarted = true;

        fuelingActivated = true;
        Debug.Log("Взаимодействие получено: заправка активирована.");

        CustomerPhrasesLoader phraseLoader = FindObjectOfType<CustomerPhrasesLoader>();
        string phrase = (phraseLoader != null) ? phraseLoader.GetRandomPhrase() : "Фраз нет!";
        Debug.Log("Фраза: " + phrase);

        FuelUIManager uiManager = FindObjectOfType<FuelUIManager>();
        if (uiManager != null)
        {
            uiManager.ShowPhrase(phrase);
        }
    }

    private IEnumerator FuelRoutine(Transform car)
    {
        Debug.Log("[FuelRoutine] запущен для " + car.name);

        // 2) Создаём и прямо вешаем как дочерний к car
        Vector3 offset = new Vector3(0, 2f, 0);
        GameObject barGO = Instantiate(fuelBarPrefab, car);
        barGO.transform.localPosition = offset;
        barGO.transform.localRotation = Quaternion.identity;
        barGO.transform.localScale = Vector3.one;
        Debug.Log("[FuelRoutine] Instantiate вернул: " + barGO.name);

        // 3) Настраиваем контроллер
        var barCtrl = barGO.GetComponent<FuelBarController>();
        if (barCtrl == null)
        {
            Debug.LogError("[FuelRoutine] НЕ нашёл FuelBarController на prefab!");
            yield break;
        }
        barCtrl.target = car;
        barCtrl.offset = offset;
        barCtrl.playerCamera = Camera.main.transform;

        // 5) Анимируем заполнение
        float duration = 3f, t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / duration);
            barCtrl.SetProgress(p);
            yield return null;
        }
        Debug.Log("[FuelRoutine] заполнение завершено");

        // 6) Убираем bar
        Destroy(barGO);
    }

    public void Interact()
    {
        ActivateFueling();
    }
}
