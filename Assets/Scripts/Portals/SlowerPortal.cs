using System.Collections;
using UnityEngine;

public class SlowerPortal : Portal
{
    private AudioSource BGMusic;
    private float slowFactor = 0.4f;

    private void Start()
    {
        BGMusic = musicGO.GetComponent<AudioSource>();
    }

    public override void Activate()
    {
        StartCoroutine(SlowTime());
    }

    protected IEnumerator SlowTime()
    {
        setPortalEnabled(false);

        Time.timeScale = slowFactor;
        BGMusic.pitch = slowFactor * 2;

        yield return new WaitForSeconds(10f * slowFactor);

        BGMusic.pitch = Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
