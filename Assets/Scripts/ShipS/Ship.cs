using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public ShipStats Stats {get { return stats; } protected set { stats = value; } }

    public string ShipID { get; private set; }

    protected ShipStats stats;
    protected MoveControl _move;
    protected Cannoneer cannoneer;

    private List<MonoBehaviour> allComponents;

    [Range(0f, 1f)]
    private float power = 0f;

    private BoardSide side;


    protected abstract void InitController();
    protected abstract void InitStats();

    private void InitCannoneer()
    {
        cannoneer = new Cannoneer(this);
    }

    private void ConnectWithShipComponents()
    {
        allComponents = new List<MonoBehaviour>();
        var components = GetComponents<IShipComponent>();
        foreach (IShipComponent component in components)
        {
            component.SetupMotherShip(this);
            // add component to list
        }
    }


    private void Start()
    {
        ShipID = gameObject.GetInstanceID().ToString();
        InitController();
        InitStats();
        InitCannoneer();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            float curPow = power + Time.deltaTime/2;
            power = Mathf.Clamp(curPow, 0.2f, 1f);
            Debug.Log("Current power = " + power);
            side = BoardSide.Front;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            Shoot(side, power);
            power = 0f;
        }
    }

    private void Shoot(BoardSide boardSide, float power)
    {
        cannoneer.Shoot(boardSide, power);
    }


}
