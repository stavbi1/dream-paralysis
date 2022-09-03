
public class GunChangingPortal : Portal
{
    private Player player;

    private void Start()
    {
        player = sceneHelperGO.GetComponent<SceneHelper>().player.GetComponent<Player>();
    }

    public override void Activate()
    {
        player.SwitchGun();

        Destroy(gameObject);
    }
}
