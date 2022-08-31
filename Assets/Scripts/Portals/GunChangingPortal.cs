using System.Collections;
using UnityEngine;

public class GunChangingPortal : Portal
{
    public override void Activate()
    {
        Debug.Log("activate");
        Destroy(gameObject);
    }
}
