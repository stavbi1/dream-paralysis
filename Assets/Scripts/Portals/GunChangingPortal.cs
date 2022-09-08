
public class GunChangingPortal : Portal
{
    private Player player;

    private void Start()
    {
        player = playerGO.GetComponent<Player>();
    }

    public override void Activate()
    {
        player.SwitchGun();

        Destroy(gameObject);
    }
}
