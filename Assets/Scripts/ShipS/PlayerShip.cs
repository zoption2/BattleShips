using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    protected override void InitController()
    {
        _move = new MoveControl(this);
    }

    protected override void InitStats()
    {
        Stats = new ShipStats(ShipID);
    }
}
