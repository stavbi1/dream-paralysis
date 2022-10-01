
public class TeleportPortal : Portal
{
    private SceneHelper sceneHelper;

    private void Start()
    {
        sceneHelper = sceneHelperGO.GetComponent<SceneHelper>();
    }

    public override void Activate()
    {
        playerGO.transform.position = transform.position;
        sceneHelper.SpawnMobs();
        Destroy(gameObject);
    }
}
