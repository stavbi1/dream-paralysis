
public class GunChangingPortal : Portal
{
    private Player player;

    private void Start()
    {
        player = playerGO.GetComponent<Player>();
    }

    public override void Interact()
    {
        player.SwitchGun();

        Destroy(gameObject);
    }
}
