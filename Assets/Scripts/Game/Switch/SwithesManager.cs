using Unity.VisualScripting;
using UnityEngine;

public class SwithesManager : MonoBehaviour
{
    [SerializeField] private FuelPistolTrigger[][] nozzleTriggers;
    private bool isEnabledFirst;
    private bool isEnabledSecond;

    [SerializeField] private GameObject[] _fuelColumnCount;
    private GameObject[][] dsf;


    public void SavingNozzles()
    {
        /*_columnCount.this.gameObject.transform.FindDeepChild($"FuelNozzle{i}");
        for (int i = 0; i < _columnCount.Length; i++)
        {
            GameObject[][] 
        }
        foreach (GameObject column in _columnCount)
        {
            
        }*/
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    /*private void Start()
    {
            for (int i = 0; i < 4; i++)//проходимся по каждому пистолету в колонке и записываем их в массив. nozzleCount = 4;
            {
                Transform nozzleCount = _columnCount.transform.FindDeepChild($"FuelNozzle{i}");
                nozzles.Add(nozzleCount);
            }

    }*/
}
