using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{
    public UnityAction OnTargetChange;

    private GameObject thisShip;
    private List<ITargetable> observableObjects;
    private GameObject currentTarget;
    private ShipStats stats;
    private float radiusOfVision = 1f;
    private int targetableLayer = 8;

    private void DoTargetChange()
    {
        OnTargetChange?.Invoke();
    }

    private void Start()
    {
        observableObjects = new List<ITargetable>();
        thisShip = gameObject;
        stats = GetComponent<Ship>().Stats;
        SetRadiusOfVision(10);
    }


    public GameObject GetCurrentTarget()
    {
        return currentTarget;
    }

    public void ChangeCurrentTarget()
    {
        var countOfTargets = observableObjects.Count;
        var randomValue = Random.Range(0, countOfTargets - 1);
        var newTarget = observableObjects[randomValue];
        SetCurrentTarget(newTarget);
    }

    public void GetClosestTarget()
    {
        if (observableObjects.Count <= 0)
        {
            return;
        }

        ITargetable closest = null;
        float distance = Mathf.Infinity;
        Vector3 myPosition = transform.position;

        for (int i = 0; i < observableObjects.Count; i++)
        {
            var target = observableObjects[i];
            var targetPosition = target.GetTarget().transform.position;
            float difference = Vector3.Distance(targetPosition, myPosition);
            if (difference < distance)
            {
                closest = target;
            }
        }

        if (closest != null)
        {
            SetCurrentTarget(closest);
        }
    }

    public void SetRadiusOfVision(float radius)
    {
        radiusOfVision = radius;
    }

    public void TryFindTargets()
    {
        observableObjects = new List<ITargetable>();

        var targets = Physics.OverlapSphere(transform.position, radiusOfVision, targetableLayer);
        foreach (var item in targets)
        {
            item.gameObject.TryGetComponent(out ITargetable target);
            if (target != null && target.GetTeam() != stats.Team)
            {
                observableObjects.Add(target);
            }
        }
    }

    private void SetCurrentTarget(ITargetable target)
    {
        var choosenTarget = target.GetTarget();
        currentTarget = choosenTarget;
        DoTargetChange();
    }
}



