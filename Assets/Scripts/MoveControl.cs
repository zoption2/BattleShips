using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class MoveControl 
{
    private Ship Ship;
    private Vector3 input;

    protected virtual void InitMoveObserverable(Component playerClass)
    {
        playerClass.FixedUpdateAsObservable()
                   .Where(x => Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                   .Subscribe(x =>
                   {
                       input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                       //Ship.Stats(input);
                   });
    }
}
