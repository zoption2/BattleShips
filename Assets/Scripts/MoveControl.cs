using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class MoveControl 
{
    private Ship Ship;
    private Vector3 input;

    public MoveControl(Ship someShip)
    {
        this.Ship = someShip;
        InitMoveObserverable(someShip);
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
        var movement =  currentSpeed * newDirection;
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

    protected virtual void InitMoveObserverable(Component playerClass)
    {
        playerClass.FixedUpdateAsObservable()
                   .Where(x => Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                   .Subscribe(x =>
                   {
                        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                        Move(input);
                   }); 
    }
}
