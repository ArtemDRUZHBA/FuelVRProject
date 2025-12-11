using UnityEngine;
using System.Collections;

public class RandomSparks : MonoBehaviour
{
    public ParticleSystem ps;

    void Start()
    {
        StartCoroutine(EmitBursts());
    }

    IEnumerator EmitBursts()
    {
        while (true)
        {
            // случайное количество искр
            int count = Random.Range(10, 21);

            // запустить burst
            ps.Emit(count);

            // случайная задержка между сериями
            float delay = Random.Range(0f, 8f);
            yield return new WaitForSeconds(delay);
        }
    }
}
