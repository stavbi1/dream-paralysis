using UnityEngine;

public class HealtherPortal : Portal
{
    private Player player;

    private void Start()
    {
        player = playerGO.GetComponent<Player>();
    }

    public override void DestroySelf()
    {
        // Dont destory healther
    }

    public override void Interact()
    {
        AudioSource.PlayClipAtPoint(healthPotionSound, playerGO.transform.position);

        int addedHP = Random.Range(10, 30);
        player.AddHP(addedHP);

        Destroy(gameObject);
    }
}
