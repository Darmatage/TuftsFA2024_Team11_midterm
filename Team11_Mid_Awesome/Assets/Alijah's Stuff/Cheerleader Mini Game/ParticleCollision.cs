using UnityEngine;
using System.Collections.Generic;

public class ParticleCollision : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents;

    public GameObject bloodDecal;
    public float spawnCooldown = 0.05f; // Time in seconds between spawns
    public int maxDecals = 50; // Maximum number of decals
    public Vector3 offsetRange = new Vector3(0.025f, 0.025f, 0f); // Range for random offsets
    public Vector2 scaleRange = new Vector2(0.75f,1.5f); // Range for random scale multipliers

    private float lastSpawnTime;
    private Queue<GameObject> decalQueue;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        decalQueue = new Queue<GameObject>();
        lastSpawnTime = -spawnCooldown; // Initialize to allow immediate spawn
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            // Trigger the OnParticleHit method on the collided object
            other.SendMessage("OnParticleHit", SendMessageOptions.DontRequireReceiver);
        }

        if (other.CompareTag("Ground"))
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                if (Time.time >= lastSpawnTime + spawnCooldown)
                {
                    Vector3 collisionPos = collisionEvents[i].intersection;
                    SpawnDecal(collisionPos);
                    lastSpawnTime = Time.time;
                }
            }
        }
    }

    void SpawnDecal(Vector3 position)
    {
        if (decalQueue.Count >= maxDecals)
        {
            GameObject oldestDecal = decalQueue.Dequeue();
            Destroy(oldestDecal);
        }

        // Apply random offset
        Vector3 randomOffset = new Vector3(
            Random.Range(-offsetRange.x, offsetRange.x),
            Random.Range(-offsetRange.y, offsetRange.y),
            Random.Range(-offsetRange.z, offsetRange.z)
        );
        position += randomOffset;
        position.y -= 0.75f;

        // Instantiate the decal
        GameObject newDecal = Instantiate(bloodDecal, position, Quaternion.identity);

        // Apply random scale
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        newDecal.transform.localScale *= randomScale;

        decalQueue.Enqueue(newDecal);
    }
}
