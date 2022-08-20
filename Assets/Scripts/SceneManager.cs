using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> mobPrefabs;
    public GameObject cameraGo;
    public GameObject x;

    private Camera fpCamera;

    private void Start()
    {
        fpCamera = cameraGo.GetComponent<Camera>();
    }

    public void OnFinishedMobs()
    {
        SpawnMobs();
        SpawnPortals();
    }

    public void SpawnMobs()
    {
        GameObject mob = mobPrefabs[Random.Range(0, mobPrefabs.Count)];

        Instantiate(x, fpCamera.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(new Vector3(0, 0, 0)));
    }

    private void SpawnPortals()
    {

    }
}
