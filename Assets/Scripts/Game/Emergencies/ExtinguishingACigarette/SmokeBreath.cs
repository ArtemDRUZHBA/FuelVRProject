using System.Collections;
using UnityEngine;

public class SmokeBreath : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeSystem;

    void Start()
    {
        smokeSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        StartCoroutine(SmokeRoutine());
    }

    IEnumerator SmokeRoutine()
    {
        // ждём первые 10 секунд перед началом дыхания
        yield return new WaitForSeconds(20f);

        while (true)
        {
            smokeSystem.Play();   // один выдох
            yield return new WaitForSeconds(8f); // пауза между выдохами
        }
    }
}
