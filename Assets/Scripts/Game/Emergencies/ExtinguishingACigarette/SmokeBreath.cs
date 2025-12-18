using System.Collections;
using UnityEngine;

public class SmokeBreath : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeSystem;
    private Coroutine routine;

    void Start()
    {
        smokeSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        routine = StartCoroutine(SmokeRoutine());
    }

    IEnumerator SmokeRoutine()
    {
        yield return new WaitForSeconds(20f);

        while (true)
        {
            smokeSystem.Play();
            yield return new WaitForSeconds(8f);
        }
    }

    public void StopSmoke()
    {
        if (routine != null)
            StopCoroutine(routine);

        smokeSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
