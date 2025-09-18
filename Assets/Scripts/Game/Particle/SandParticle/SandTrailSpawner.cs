using System.Collections.Generic;
using UnityEngine;

public class SandTrailSpawner : MonoBehaviour
{
    [Header("Particle System песка")]
    [SerializeField] private ParticleSystem sandPS;

    [Header("Префаб песчаного следа")]
    [SerializeField] private GameObject sandDecalPrefab;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private int collisionCounter = 0;

    private void Awake()
    {
        var coll = sandPS.collision;
        coll.enabled = true;
        coll.sendCollisionMessages = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");

        int count = sandPS.GetCollisionEvents(other, collisionEvents);
        if (count == 0) return;

        var evt = collisionEvents[0];
        Vector3 pos = evt.intersection;
        Quaternion rot = Quaternion.Euler(90f, 0f, 0f);
        if (other.layer == groundLayer)
        {
            collisionCounter++;
            if (collisionCounter <= 100) Instantiate(sandDecalPrefab, pos, rot);
            

            
        }
    }
}
