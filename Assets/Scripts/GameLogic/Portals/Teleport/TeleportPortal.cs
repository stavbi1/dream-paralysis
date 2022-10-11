
public class TeleportPortal : Portal
{
    private SceneHelper sceneHelper;
    private Player player;

    private void Start()
    {
        sceneHelper = sceneHelperGO.GetComponent<SceneHelper>();
        player = playerGO.GetComponent<Player>();
    }

    public override void Interact()
    {
        player.AddHP(20);
        playerGO.transform.position = transform.position;
        sceneHelper.SpawnMobs();
        Destroy(gameObject);
    }
}
