using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator animator;
    public GameObject teleport;

    private const string OPEN_ANIMATION = "Open";

    public void Open()
    {
        animator.SetTrigger(OPEN_ANIMATION);
        Invoke(nameof(OpenTeleport), 2f);
    }

    public void OpenTeleport()
    {
        teleport.SetActive(true);
    }
}
