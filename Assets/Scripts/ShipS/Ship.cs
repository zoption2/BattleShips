using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public Rigidbody Rigidbody { get { return _rigidbody; } set { _rigidbody = value; } }
    public Transform Transform { get { return transform; }}
    public ShipStats Stats {get { return stats; } protected set { stats = value; } }
    public Vector3 Position {get { return transform.position; } set { transform.position = value; } }
    public Quaternion Rotation {get { return transform.rotation; } set { transform.rotation = value; } }


    public string ShipID { get; private set; }

    protected ShipStats stats;
    protected MoveControl _move;
    protected Cannoneer cannoneer;
    private Rigidbody _rigidbody;

    [Range(0f, 1f)]
    private float power = 0f;

    private BoardSide side;


    protected abstract void InitController();
    protected abstract void InitStats();

    private void InitCannoneer()
    {
        cannoneer = new Cannoneer(this);
    }

    private void Start()
    {
        ShipID = gameObject.GetInstanceID().ToString();
        InitRigidbody();
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

    private void InitRigidbody()
    {
        TryGetComponent<Rigidbody>(out _rigidbody);
        if (!_rigidbody)
        {
            Debug.LogError("No rigidbody at " + this);
        }
    }
}
