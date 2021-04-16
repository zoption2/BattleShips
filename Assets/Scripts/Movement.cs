using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IShipComponent
{
    private Ship Ship;
    private Vector3 input;

    public void SetupMotherShip(Ship mothership)
    {
        Ship = mothership;
    }

    public void Move(Vector3 direction)
    {
        DoRotation(direction);
        var newDirection = GetCurrentDirection();
        float currentSpeed = Ship.Rigidbody.velocity.magnitude;
        if (currentSpeed < Ship.Stats.Speed)
        {
            currentSpeed += Time.deltaTime;
        }
        var movement = currentSpeed * newDirection;
        Ship.Rigidbody.velocity = movement;
    }

    private void DoRotation(Vector3 direction)
    {
        var angle = Quaternion.Lerp(Ship.Rotation, Quaternion.LookRotation(direction), Ship.Stats.RotationSpeed * Time.deltaTime);
        Ship.Rotation = angle;
    }

    private Vector3 GetCurrentDirection()
    {
        Vector3 currentDirection = Ship.Transform.forward;
        return currentDirection;
    }


}
