using UnityEngine;

public class Turkey : Gun
{
    public GameObject throwable;

    private float throwForce = 20f;
    private float throwUpwardForce = 5f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Shoot()
    {
        base.Shoot();

        GameObject projectile = Instantiate(throwable, transform.position, fpsCamera.transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceToAdd = fpsCamera.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }
}
