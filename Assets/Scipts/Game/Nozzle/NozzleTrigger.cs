using UnityEngine;

public class NozzleTrigger : MonoBehaviour
{
    [SerializeField] private Nozzle _nozzle;
    [SerializeField] private Collider _fuelSocketTrigger;
    [SerializeField] private Collider _nozzleHandleTrigger;

    private void Start()
    {
        _fuelSocketTrigger.isTrigger = false;
        _nozzleHandleTrigger.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NozzleHandle"))
        {
            Debug.Log("Джойстик взял пистолет");
            _fuelSocketTrigger.isTrigger = true;
            _nozzleHandleTrigger.isTrigger = false;
            //вызов встроенного метода XR с взятием, а именно взятием этого пистолета
        }
        if (other.CompareTag("FuelSocket"))
        {
            Debug.Log("Джойстик вернул пистолет в гнездо");
            _fuelSocketTrigger.isTrigger = false;
            _nozzleHandleTrigger.isTrigger = true;
            _nozzle.ReturnToRest();
        }
        if (other.CompareTag("FuelTank"))
        {
            Debug.Log("Джойстик вставил пистолет в бак");
            _nozzleHandleTrigger.isTrigger = true;
            //вызов метода с перемещеним пистолета в дочерний элемент этого бака и изменением позиции
        }
    }
}
