using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class Creator : MonoBehaviour
{
    [SerializeField] private List<GameObject> InitOnStart;

    private void Awake()
    {
        for (int i = 0; i < InitOnStart.Count; i++)
        {
            Instantiate(InitOnStart[i]);
        }
    }
}
