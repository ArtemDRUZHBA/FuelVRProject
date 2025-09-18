using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject particleSystem;
    private ParticleSystem particle;

    private void Start()
    {
        particle = particleSystem.GetComponent<ParticleSystem>();
        particle.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        particle.Play(); Debug.Log(particle.isPlaying);
    }
    private void OnTriggerExit(Collider other)
    {
        particle.Stop(); Debug.Log(particle.isPlaying);
    }
}
