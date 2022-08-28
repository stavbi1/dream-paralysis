using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> mobPrefabs;
    public List<GameObject> portalPrefabs;
    public GameObject player;

    private const float PORTAL_SPAWN_CHANCE = 30f;

    public void OnFinishedMobs()
    {
        SpawnMobs();
        SpawnPortals();
    }

    public void SpawnMobs()
    {
        GameObject mobGO = mobPrefabs[Random.Range(0, mobPrefabs.Count)];
        Mob mob = mobGO.GetComponent<Mob>();
        mob.playerGO = player;
        mob.sceneManagerGO = gameObject;

        SpawnInRadius(25, 40, mobGO);
    }

    private void SpawnPortals()
    {
        if (Random.value < PORTAL_SPAWN_CHANCE)
        {
            GameObject portalGO = portalPrefabs[Random.Range(0, portalPrefabs.Count)];
            Portal portal = portalGO.GetComponent<Portal>();
            portal.musicGO = gameObject;

            SpawnInRadius(5, 10, portalGO);
        }
    }

    public void SpawnInRadius(float innerRadius, float outerRadius, GameObject toSpawn)
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

        Instantiate(toSpawn, randomPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}
