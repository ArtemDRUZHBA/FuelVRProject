using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class FuelParticleSystem : MonoBehaviour
{
    [Header("Particle System утечки")]
    [SerializeField] private ParticleSystem leakPS;

    [Header("Префаб лужи")]
    [SerializeField] private GameObject puddlePrefab;
    [SerializeField] private GameObject sandMarkPrefab;

    [Header("Режим работы")]
    [SerializeField] private bool isSandMode = false;

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
        int sandMarkLayer = LayerMask.NameToLayer("SandMark");

        int count = leakPS.GetCollisionEvents(other, collisionEvents);
        if (count == 0) return;

        var evt = collisionEvents[0];
        Vector3 hitPoint = evt.intersection;
        Quaternion rot = Quaternion.Euler(90f, 0f, 0f);

        if (isSandMode)
        {
            if (other.layer == puddleLayer)
            {
                // Песок попал в лужу удаляем лужу и создаём песок
                Destroy(other.gameObject);
                Instantiate(sandMarkPrefab, hitPoint, rot);
                return;
            }

            if (other.layer == groundLayer)
            {
                // Песок попал в землю создаём песчаную кучку
                Instantiate(sandMarkPrefab, hitPoint, rot);
                return;
            }

            if (other.layer == sandMarkLayer)
            {
                // Песок попал в существующую кучку увеличиваем её
                var grower = other.GetComponent<FuelPuddleGrower>();
                if (grower != null)
                {
                    grower.Grow();
                }
                return;
            }
        }
        else
        {
            if (other.layer == groundLayer)
            {
                // Топливо попало в землю создаём лужу
                GameObject puddle = Instantiate(puddlePrefab, hitPoint, rot);
                return;
            }

            if (other.layer == puddleLayer)
            {
                // Топливо попало в лужу увеличиваем её
                var grower = other.GetComponent<FuelPuddleGrower>();
                if (grower != null)
                {
                    grower.Grow();
                }
                return;
            }
        }
    }
}
