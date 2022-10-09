using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    public ParticleSystem impactEffect;
    public AudioClip impactSound;

    private float damage = 35f;

    private void OnCollisionEnter(Collision collision)
    {
        Gun.OnHit(collision.transform, damage);

        AudioSource.PlayClipAtPoint(impactSound, transform.position);
        GameObject impact = Instantiate(
            impactEffect.gameObject,
            transform.position,
            transform.rotation
        );
        Destroy(impact, 1.0f);
        Destroy(gameObject);
    }
}
