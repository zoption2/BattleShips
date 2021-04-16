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
    public float Speed { get { return speed; }}
    public float RotationSpeed { get { return rotationSpeed; }}

    private string shipID;
    private float speed = 2;
    private float rotationSpeed = 1;
    private Team team;
}
