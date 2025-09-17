using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class FuelParticleSystem : MonoBehaviour
{
    [Header("Particle System утечки")]
    [SerializeField] private ParticleSystem leakPS;

    [Header("Префаб лужи")]
    [SerializeField] private GameObject puddlePrefab;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Awake()
    {
        if (leakPS == null) leakPS = GetComponent<ParticleSystem>();

        var coll = leakPS.collision;
        coll.enabled = true;
        coll.sendCollisionMessages = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");
        int puddleLayer = LayerMask.NameToLayer("Puddle");

        int count = leakPS.GetCollisionEvents(other, collisionEvents);
        if (count == 0) return;

        var evt = collisionEvents[0];
        Vector3 hitPoint = evt.intersection;
        Quaternion rot = Quaternion.LookRotation(evt.normal) * Quaternion.Euler(90f, 0f, 0f);

        if (other.layer == groundLayer)
        {
            // Спавним новую лужу
            GameObject puddle = Instantiate(puddlePrefab, hitPoint, rot);
        }
        else if (other.layer == puddleLayer)
        {
            // Увеличиваем существующую лужу
            var grower = other.GetComponent<FuelPuddleGrower>();
            if (grower != null)
            {
                grower.Grow();
            }
        }
    }
}
