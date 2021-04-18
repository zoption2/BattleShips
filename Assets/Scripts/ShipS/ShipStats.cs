using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats
{
    public ShipStats(string shipID)
    {
        this.shipID = shipID;
    }

    public string ShipID { get { return shipID; }}
    public Team Team { get { return team; } set { team = value; } }
    public float Speed { get { return speed; } set { speed = value; } }
    public float RotationSpeed { get { return rotationSpeed; }}
    public float RadiusOfVision {get { return radiusOfVision; } set { radiusOfVision = value; } }

    private string shipID;
    private float speed = 2f;
    private float rotationSpeed = 1f;
    private float radiusOfVision = 10f;
    private Team team;
}
