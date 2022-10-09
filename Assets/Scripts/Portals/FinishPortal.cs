using UnityEngine.SceneManagement;

public class FinishPortal : Portal
{
    public override void Interact()
    {
        SceneManager.LoadScene(1);
    }
}
