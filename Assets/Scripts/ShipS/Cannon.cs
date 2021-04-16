using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : Artillery
{
    [SerializeField] private Cannonball cannonball;

    protected override void ShootMechanic(float power)
    {
        //var newProjectile = Instantiate(cannonball, transform.position, Quaternion.identity);
        var newProjectile = projectilesPool.Spawn(OurShipID, aim.position, Quaternion.identity);
        var direction = transform.forward;
        newProjectile.GetComponent<Projectiles>().DoProjectileFlight(direction, power);
    }
}
