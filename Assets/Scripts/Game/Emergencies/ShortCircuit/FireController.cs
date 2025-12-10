using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private GameObject[] _fires;
    void Start()
    {
        foreach (var fire in _fires)
            fire.SetActive(false);
        StartCoroutine(FireTime());
    }

    IEnumerator FireTime()
    {
        for (int i = 0; i < _fires.Length; i++)
        {
            // включаем текущий
            _fires[i].SetActive(true);

            // ждём 5 секунд
            yield return new WaitForSeconds(5f);

            // выключаем текущий
            _fires[i].SetActive(false);
        }
    }
}
