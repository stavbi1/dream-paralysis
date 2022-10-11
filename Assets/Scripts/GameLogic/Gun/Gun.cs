using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;
    public AudioClip gunShotAudio;

    protected Animator animator;
    protected const string SHOOTING_ANIMATION = "Shoot";

    private float shootCooldown = 1f;
    private bool readyToShoot;

    protected virtual void Start()
    {
        readyToShoot = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (readyToShoot && Input.anyKeyDown)
        {
            readyToShoot = false;
            Shoot();
            Invoke(nameof(ResetShoot), shootCooldown);
        }
    }

    protected virtual void Shoot() {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        AudioSource.PlayClipAtPoint(gunShotAudio, transform.position);
        animator.SetTrigger(SHOOTING_ANIMATION);
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    public static void OnHit(Transform hit, float damage)
    {
        if (hit.CompareTag("Mob"))
        {
            Mob mob = hit.GetComponentInParent<Mob>();
            mob.TakeDamage(damage);
        }
        else if (hit.CompareTag("Interactable"))
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            interactable.Interact();
        }
    }
}
