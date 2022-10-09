using UnityEngine;
using UnityEngine.UI;

public abstract class Portal : MonoBehaviour, IInteractable
{
    public GameObject sceneHelperGO;
    public AudioClip clockChime;
    public AudioClip healthPotionSound;
    public GameObject playerGO;
    public Text timerText;
    public bool isEnabled;

    public abstract void Interact();

    // Some portals need to finish their effect before destroying
    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected void setPortalEnabled(bool toggle)
    {
        isEnabled = false;

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Renderer>().enabled = toggle;
        }
        GetComponent<Renderer>().enabled = toggle;
    }
}
