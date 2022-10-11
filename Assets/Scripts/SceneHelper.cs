using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneHelper : MonoBehaviour
{
    public List<GameObject> progressPortals;
    public List<GameObject> mobPrefabs;
    public List<GameObject> portalPrefabs;
    public GameObject playerGO;
    public AudioClip clockChime;
    public AudioClip healthPotionSound;
    public GameObject timerTextGO;

    private Player player;
    private GameObject currentPortal;
    private int wavesCleared;
    private int leverShots;
    private Text timerText;
    private int mobWaveCount;
    private const float PORTAL_SPAWN_CHANCE = 0.95f;
    private const int WAVES_PROGRESS = 5;
    private const int MAX_LEVELS = 2;

    private void Start()
    {
        mobWaveCount = 1;
        wavesCleared = 0;
        leverShots = 0;
        timerText = timerTextGO.GetComponent<Text>();
        player = playerGO.GetComponent<Player>();
    }

    public void AddLeverShot()
    {
        leverShots++;
    }

    public int GetLeverShots()
    {
        return leverShots;
    }

    public void OnMobDie()
    {
        mobWaveCount--;

        if (mobWaveCount <= 0 && GetCurrentLevel() < MAX_LEVELS)
        {
            wavesCleared++;

            if (IsNextLevel())
            {
                if (currentPortal)
                {
                    currentPortal.GetComponent<Portal>().DestroySelf();
                }

                player.SwitchGunToPistol();
                progressPortals[GetCurrentLevel() - 1].SetActive(true);
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
            mob.playerGO = playerGO;
            mob.sceneHelperGO = gameObject;
            mobWaveCount = Random.Range(1, 3);

            for (int i = 0; i < mobWaveCount; i++)
            {
                SpawnInRadius(15, 35, mobGO);
            }
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

    private void SpawnPortals()
    {
        if (Random.value < PORTAL_SPAWN_CHANCE)
        {
            if (currentPortal)
            {
                currentPortal.GetComponent<Portal>().DestroySelf();
            }

            GameObject portalGO = portalPrefabs[Random.Range(0, portalPrefabs.Count)];
            Portal portal = portalGO.GetComponent<Portal>();
            portal.sceneHelperGO = gameObject;
            portal.clockChime = clockChime;
            portal.healthPotionSound = healthPotionSound;
            portal.timerText = timerText;
            portal.playerGO = playerGO;

            currentPortal = SpawnInRadius(5, 10, portalGO);
        }
    }

    private int GetCurrentLevel()
    {
        return wavesCleared / WAVES_PROGRESS;
    }

    private bool IsNextLevel()
    {
        return wavesCleared % WAVES_PROGRESS == 0;
    }
}
