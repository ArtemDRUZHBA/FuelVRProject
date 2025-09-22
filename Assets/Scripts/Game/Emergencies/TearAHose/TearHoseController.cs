using UnityEngine;

public class TearHoseController : MonoBehaviour
{
    [SerializeField] private Transform hoseEnd; // конец шланга, который вставлен в колонку

    public void DetachFromPump()
    {
        if (hoseEnd != null)
        {
            hoseEnd.SetParent(null); // отсоединяем от колонки
            // можно добавить анимацию падения или физику
        }
    }
}
