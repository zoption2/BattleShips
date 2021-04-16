using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class LevelManager: MonoBehaviour
{
    static private List<GameObject> teamOne;
    static private List<GameObject> teamTwo;

    private void Awake()
    {
        teamOne = new List<GameObject>();
        teamTwo = new List<GameObject>();
    }

    private void Start()
    {
        PrefabHolder.Instance.InstantiatePrefab("Boat", Vector3.zero, Quaternion.identity);
    }

    public static void AddShipInGame (GameObject gameObject, int team)
    {
        if(team == 1)
        {
            teamOne.Add(gameObject);
        }
        else if (team == 2)
        {
            teamTwo.Add(gameObject);
        }
    }
}
