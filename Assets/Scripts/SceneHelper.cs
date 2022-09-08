using System.Collections.Generic;
using UnityEngine;

public class SceneHelper : MonoBehaviour
{
    public List<GameObject> mobPrefabs;
    public List<GameObject> portalPrefabs;
    public GameObject player;
    public AudioClip clockChime;

    private GameObject currentPortal;
    private const float PORTAL_SPAWN_CHANCE = 0.95f;

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
        mob.sceneHelperGO = gameObject;

        SpawnInRadius(25, 40, mobGO);
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
}
