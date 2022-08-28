using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public GameObject musicGO;

    private AudioSource BGMusic;
    private float slowFactor = 0.4f;

    private void Start()
    {
        BGMusic = musicGO.GetComponent<AudioSource>();
    }

    public void Activate()
    {
        StartCoroutine(TimeSlower());
        setPortalEnabled(false);
    }

    private void setPortalEnabled(bool toggle)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Renderer>().enabled = toggle;
        }
        GetComponent<Renderer>().enabled = toggle;
    }

    IEnumerator TimeSlower()
    {

        Time.timeScale = slowFactor;
        BGMusic.pitch = slowFactor * 2;

        yield return new WaitForSeconds(10f * slowFactor);

        BGMusic.pitch = Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
