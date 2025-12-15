using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    private FireController _corountine;
    void Start()
    {
        _corountine = GetComponent<FireController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(_corountine.FireTime());
        }
    }
}
