using UnityEngine;

public abstract class Portal : MonoBehaviour
{
    public GameObject sceneHelperGO;

    public abstract void Activate();

    protected void setPortalEnabled(bool toggle)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Renderer>().enabled = toggle;
        }
        GetComponent<Renderer>().enabled = toggle;
    }
}
