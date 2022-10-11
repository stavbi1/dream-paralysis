using UnityEngine;


public class LeversTeleportPortal : TeleportPortal
{
    public GameObject[] levers;

    private Player player;

    public override void Interact()
    {
        //player.SwitchGunToPistol();

        foreach (GameObject lever in levers)
        {
            lever.SetActive(true);
        }

        base.Interact();
    }
}
