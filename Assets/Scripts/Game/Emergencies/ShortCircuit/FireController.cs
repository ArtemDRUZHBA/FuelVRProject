using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private GameObject[] _fires;
    void Start()
    {
        foreach (var fire in _fires)
            fire.SetActive(false);
    }

    public IEnumerator FireTime()
    {
        for (int i = 0; i < _fires.Length; i++)
        {
            _fires[i].SetActive(true);

            yield return new WaitForSeconds(5f);

            _fires[i].SetActive(false);
        }
    }
}
