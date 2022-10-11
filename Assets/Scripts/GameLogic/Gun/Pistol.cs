using UnityEngine;

public class Pistol : Gun
{
    public GameObject crosshair;

    private readonly int TRIGGER_LAYER_MASK_IGNORE = -5;
    private float damage = 20f;
    private float range = 100f;

    protected override void Shoot()
    {
        base.Shoot();

        RaycastHit hit;

        if (Physics.Raycast(
            fpsCamera.transform.position,
            fpsCamera.transform.forward,
            out hit,
            range,
            TRIGGER_LAYER_MASK_IGNORE,
            QueryTriggerInteraction.Ignore
        ))
        {
            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 1.0f);
            }

            OnHit(hit.transform, damage);
        }
    }
}
