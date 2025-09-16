using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class FuelParticleSystem : MonoBehaviour
{
    [Header("Particle System утечки")]
    [SerializeField] private ParticleSystem leakPS;

    [Header("Префаб лужи")]
    [SerializeField] private GameObject puddlePrefab;

    [Header("Рост лужи за одно попадание")]
    [SerializeField] private float growthPerHit = 0.02f;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private GameObject currentPuddle;

    private void Awake()
    {
        if (leakPS == null) leakPS = GetComponent<ParticleSystem>();

        // Включаем коллизии и сообщения
        var coll = leakPS.collision;
        coll.enabled = true;
        coll.sendCollisionMessages = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        // Реагируем только на пол (слой "Ground")
        if (other.layer != LayerMask.NameToLayer("Ground")) return;

        int eventsCount = leakPS.GetCollisionEvents(other, collisionEvents);
        if (eventsCount == 0) return;

        // Первая точка столкновения
        var evt = collisionEvents[0];
        Vector3 hitPoint = evt.intersection;
        Quaternion rot = Quaternion.LookRotation(evt.normal) * Quaternion.Euler(90f, 0f, 0f);

        bool hitExistingPuddle = false;

        // Проверяем, есть ли лужа и попали ли мы в её коллайдер
        if (currentPuddle != null)
        {
            var col = currentPuddle.GetComponent<Collider>();
            if (col != null)
            {
                // ClosestPoint вернёт сам hitP, если он внутри коллайдера
                Vector3 closest = col.ClosestPoint(hitPoint);
                if (Vector3.Distance(closest, hitPoint) < 0.001f)
                    hitExistingPuddle = true;
            }
        }

        if (hitExistingPuddle)
        {
            // Растём
            var s = currentPuddle.transform.localScale;
            s.x += growthPerHit;
            s.z += growthPerHit;
            currentPuddle.transform.localScale = s;
        }
        else
        {
            // Новая лужа
            currentPuddle = Instantiate(puddlePrefab, hitPoint, rot);
        }
    }
}
