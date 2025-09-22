using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class FuelParticleSystem : MonoBehaviour
{
    [Header("Particle System утечки")]
    [SerializeField] private ParticleSystem leakPS;

    [Header("Префаб лужи")]
    [SerializeField] private GameObject[] puddlePrefabs;

    [Header("Материалы")]
    [SerializeField] private Material fuelMaterial, sandMaterial;

    [Header("Режим работы")]
    [SerializeField] private bool isSandMode = false;

    private List<ParticleCollisionEvent> collisionEvents = new(); //List<ParticleCollisionEvent>();

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

        if (isSandMode)
        {
            if (other.layer == puddleLayer)
            {
                SpawnRandomPuddle(hitPoint, sandMaterial);
                return;
            }

            if (other.layer == groundLayer)
            {
                // Песок попал в землю создаём песчаную кучку
                SpawnRandomPuddle(hitPoint, sandMaterial);
                return;
            }

            if (other.layer == sandMarkLayer)
            {
                // Песок попал в существующую кучку увеличиваем её
                if (other.TryGetComponent(out FuelPuddleGrower grower))
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
                SpawnRandomPuddle(hitPoint, fuelMaterial);
                return;
            }

            if (other.layer == puddleLayer)
            {
                // Топливо попало в лужу увеличиваем её
                if (other.TryGetComponent(out FuelPuddleGrower grower))
                {
                    grower.Grow();
                }
                return;
            }
        }
    }
    private GameObject SpawnRandomPuddle(Vector3 pos, Material mat)
    {
        int index = Random.Range(0, puddlePrefabs.Length);
        GameObject puddleX = puddlePrefabs[index];

        GameObject puddle = Instantiate(puddleX, pos, puddleX.transform.rotation);

        // Назначаем материал
        var renderer = puddle.GetComponentInChildren<MeshRenderer>();
        if (renderer != null && mat != null)
        {
            renderer.material = mat;
        }

        return puddle;
    }
}
