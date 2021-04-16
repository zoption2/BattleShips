using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, ITargetable
{
    private ITargetableObjects targetableObjects;

    protected virtual void BecomeTargetable(ITargetableObjects targetable)
    {
        targetableObjects = targetable;
        targetableObjects.AddToTargetable(gameObject);
    }
    public GameObject GetTarget()
    {
        return this.gameObject;
    }
}

