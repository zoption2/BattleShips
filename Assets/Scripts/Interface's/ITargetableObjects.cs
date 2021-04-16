using System.Collections.Generic;
using UnityEngine;

public interface ITargetableObjects
{
    void AddToTargetable(GameObject targetableObject);
    List<GameObject> GetPossibleTargets();
    void RemoveFromTargetable(GameObject targetableObject);
}