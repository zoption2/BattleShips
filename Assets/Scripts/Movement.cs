using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour, IShipComponent
{
    private Ship Ship;
    private Vector3 input;
    private new Rigidbody rigidbody;

    public void SetupMotherShip(Ship mothership)
    {
        Ship = mothership;
    }

    public void DoLocalSetups()
    {
        TryGetComponent<Rigidbody>(out rigidbody);
    }

    public void Move(Vector3 direction)
    {
        DoRotation(direction);
        var newDirection = GetCurrentDirection();
        float currentSpeed = rigidbody.velocity.magnitude;
        if (currentSpeed < Ship.Stats.Speed)
        {
            currentSpeed += Time.deltaTime;
        }
        var movement = currentSpeed * newDirection;
        rigidbody.velocity = movement;
    }

    private void DoRotation(Vector3 direction)
    {
        var angle = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Ship.Stats.RotationSpeed * Time.deltaTime);
        transform.rotation = angle;
    }

    private Vector3 GetCurrentDirection()
    {
        Vector3 currentDirection = transform.forward;
        return currentDirection;
    }


}
