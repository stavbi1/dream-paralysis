using System.Collections.Generic;
using UnityEngine;

public class SceneHelper : MonoBehaviour
{
    public List<GameObject> progressPortals;
    public List<GameObject> mobPrefabs;
    public List<GameObject> portalPrefabs;
    public GameObject player;
    public AudioClip clockChime;
    
    private Mob spawnedMob;
    private GameObject currentPortal;
    private int mobsKilled;
    private const float PORTAL_SPAWN_CHANCE = 0.95f;
    private const int MOBS_KILLED_PROGRESS = 2;
    private const int MAX_LEVELS = 2;

    private void Start()
    {
        mobsKilled = 0;
    }

    public void OnMobDie()
    {
        if (GetCurrentLevel() < MAX_LEVELS)
        {
            mobsKilled++;

            if (IsNextLevel())
            {
                Destroy(currentPortal);
                progressPortals[GetCurrentLevel()].SetActive(true);
            } else
            {
                SpawnMobs();
                SpawnPortals();
            }
            
        }
    }

    public void SpawnMobs()
    {
        if (GetCurrentLevel() < MAX_LEVELS)
        {
            GameObject mobGO = mobPrefabs[GetCurrentLevel() % mobPrefabs.Count];
            Mob mob = mobGO.GetComponent<Mob>();
            mob.playerGO = player;
            mob.sceneHelperGO = gameObject;

            GameObject spawnedMobGO = SpawnInRadius(25, 40, mobGO);
            spawnedMob = spawnedMobGO.GetComponent<Mob>();
        }
    }

    public GameObject SpawnInRadius(float innerRadius, float outerRadius, GameObject toSpawn)
    {
        float radius = Random.Range(innerRadius, outerRadius);
        float theta = Random.Range(0, 2 * Mathf.PI);
        float x = radius * Mathf.Cos(theta);
        float z = radius * Mathf.Sin(theta);

        Vector3 randomPosition = player.transform.position +
            new Vector3(
                x,
                0,
                z
            );

        return Instantiate(toSpawn, randomPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

    public void UpdateMobDestination()
    {
        spawnedMob.UpdateDestination();
    }

    private void SpawnPortals()
    {
        
        if (Random.value < PORTAL_SPAWN_CHANCE)
        {
            Destroy(currentPortal);
            GameObject portalGO = portalPrefabs[Random.Range(0, portalPrefabs.Count)];
            Portal portal = portalGO.GetComponent<Portal>();
            portal.sceneHelperGO = gameObject;
            portal.clockChime = clockChime;
            portal.playerGO = player;

            currentPortal = SpawnInRadius(5, 10, portalGO);
        }
    }

    private int GetCurrentLevel()
    {
        return mobsKilled / MOBS_KILLED_PROGRESS;
    }

    private bool IsNextLevel()
    {
        return mobsKilled % MOBS_KILLED_PROGRESS == 0;
    }
}
