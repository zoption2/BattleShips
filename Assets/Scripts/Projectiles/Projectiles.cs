using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectiles : MonoBehaviour
{
    protected Rigidbody _rigidbody;

    protected void Awake()
    {
        AddRigidbody();
    }

    public void DoProjectileFlight(Vector3 direction, float power)
    {
        var powerModifier = Mathf.Clamp(power, 0.1f, 1f);
        _rigidbody.AddForce(direction * 20f * powerModifier, ForceMode.Impulse);
    }

    private void AddRigidbody()
    {
         TryGetComponent<Rigidbody>(out _rigidbody);
        //_rigidbody = GetComponent<Rigidbody>();
    }


}
