using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;
    public AudioClip gunShotAudio;

    private float damage = 25f;
    private float range = 100f;
    private Animator animator;
    private const string SHOOTING_ANIMATION = "Shoot";

    void Update()
    {
        animator = GetComponent<Animator>();

        //if (Input.GetButtonDown("Fire1"))
        if (Input.anyKeyDown)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(SHOOTING_ANIMATION)) { 
            RaycastHit hit;

            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }

            AudioSource.PlayClipAtPoint(gunShotAudio, transform.position);
            animator.SetTrigger(SHOOTING_ANIMATION);

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                if (impactEffect != null)
                {
                    GameObject impact = Instantiate(impactEffect.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impact, 1.0f);
                }

                if (hit.transform.CompareTag("Mob"))
                {
                    Mob mob = hit.transform.GetComponentInParent<Mob>();
                    mob.TakeDamage(damage);
                } else if (hit.transform.CompareTag("Portal"))
                {
                    Portal portal = hit.transform.GetComponent<Portal>();
                    portal.Activate();
                }
            }
        }
    }
}
