using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SlowerPortal : Portal
{
    public GameObject timerTextGo;

    private AudioSource BGMusic;
    private float slowFactor = 0.4f;
    private float duration = 10f;
    private float timeRemaining;
    private bool isTimeSlower;

    private void Start()
    {
        BGMusic = sceneHelperGO.GetComponent<AudioSource>();
        
        isTimeSlower = false;
        timeRemaining = duration;
        isEnabled = true;
    }

    private void Update()
    {
        if (isTimeSlower && timeRemaining > 0)
        {
            timerText.text = timeRemaining.ToString("F2");
            timeRemaining -= Time.deltaTime / slowFactor;
        }
        else
        {
            timerText.text = "";
        }
    }

    public override void DestroySelf()
    {
        if (!isTimeSlower)
        {
            Destroy(gameObject);
        }
    }

    public override void Interact()
    {
        if (isEnabled)
        {
            AudioSource.PlayClipAtPoint(clockChime, playerGO.transform.position);
            StartCoroutine(SlowTime());
        }
    }

    protected IEnumerator SlowTime()
    {
        setPortalEnabled(false);

        Time.timeScale = slowFactor;
        BGMusic.pitch = slowFactor * 2;

        isTimeSlower = true;
        yield return new WaitForSeconds(duration * slowFactor);
        isTimeSlower = false;

        BGMusic.pitch = Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
