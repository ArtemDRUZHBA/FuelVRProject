using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CreateFuelPistol : MonoBehaviour
{
    [SerializeField] private GameObject _fuelPistolPrefab;
    public List<GameObject> fuelPistols = new();
    public List<Transform> fuelPistolAnchors = new();
    //public FuelPistolTrigger[] fuelPistols = new FuelPistolTrigger[4];
    //public List<FuelPistolTrigger> xyz = new ();

    [SerializeField] private CreateHose _instantiateHose;

    private void Start()
    {
        StartCoroutine(Init());
    }

    public void InstantiateAndSaveFuelPistol()
    {
        for (int i = 0; i < 4; i++)
        {
            Transform? transformFuelPistol = transform.FindDeepChild($"FuelPistolAnchor{i}");//Ищем гнёзда для пистолетов. Transform? значит что может принимать значение null -> nullable
            if (transformFuelPistol != null)//Проверяем если гнёздо не null
            {
                GameObject instantiateFuelPistol = Instantiate(_fuelPistolPrefab, transformFuelPistol, true);//Создаём префаб пистолета в родителе - гнездо.
                instantiateFuelPistol.name = $"FuelPistol{i}";
                instantiateFuelPistol.transform.localPosition = Vector3.zero;
                fuelPistols.Add(instantiateFuelPistol);
                fuelPistolAnchors.Add(transformFuelPistol);
                //xyz.Add(new FuelPistolTrigger (instantiateFuelPistol));
                //xyz.Add(instantiateFuelPistol);
                //fuelPistols.AddRange(instantiateFuelPistol.transform);
                //fuelPistols[i] = instantiateFuelPistol;
            }
            else
            {
                Debug.Log("FuelPistolAnchor null. Break spawn fuelPistol");
                break;
            }
        }
    }
    private IEnumerator Init()
    {
        InstantiateAndSaveFuelPistol();

        // ждём 1 кадр, чтобы Unity обновил иерархию
        yield return null;

        _instantiateHose.InstantiateHose();
    }
}
