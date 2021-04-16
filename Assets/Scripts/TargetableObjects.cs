using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableObjects : ITargetableObjects
{
    private List<GameObject> targets;

    public TargetableObjects()
    {
        targets = new List<GameObject>();
    }

    public void AddToTargetable(GameObject targetableObject)
    {
        if (!targets.Contains(targetableObject))
        {
            targets.Add(targetableObject);
        }
    }

    public void RemoveFromTargetable(GameObject targetableObject)
    {
        if (!targets.Contains(targetableObject))
        {
            targets.Remove(targetableObject);
        }
    }

    public List<GameObject> GetPossibleTargets()
    {
        return targets;
    }
}
