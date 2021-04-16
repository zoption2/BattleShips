using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class PrefabHolder : Singleton<PrefabHolder>
{
    [System.Serializable]
    public class AddressablePrefabs
    {
        public string tag;
        public AssetReference addressablePrefab;
        public AddressablePrefabs(string tag, AssetReference addressablePrefab)
        {
            this.tag = tag;
            this.addressablePrefab = addressablePrefab;
        }
    }

    [SerializeField] private List<AddressablePrefabs> prefabsReferences;

    private Dictionary<string, AssetReference> holder;

    private GameObject currentPrefab;

    public GameObject GetCurrentPrefab()
    {
        return currentPrefab;
    }

    public async void InstantiatePrefab(string tag, Vector3 position, Quaternion rotation, int team = 1)
    {
        AsyncOperationHandle<GameObject> prefab = holder[tag].InstantiateAsync(position, rotation);
        Task<GameObject> GO = await Task.WhenAny(prefab.Task);
        var CurrentPrefab = GO.Result;
        LevelManager.AddShipInGame(CurrentPrefab, team);
    }

    public async Task<GameObject> LoadPrefab(string tag)
    {
        AsyncOperationHandle<GameObject> prefab = holder[tag].LoadAssetAsync<GameObject>();
        Task<GameObject> GO = await Task.WhenAny(prefab.Task);
        currentPrefab = GO.Result;
        return currentPrefab;
    }

    private void Awake()
    {
        Instance = this;
        FillPrefabHolder();
    }

    private void FillPrefabHolder()
    {
        holder = new Dictionary<string, AssetReference>();

        for (int i = 0; i < prefabsReferences.Count; i++)
        {
            holder.Add(prefabsReferences[i].tag, prefabsReferences[i].addressablePrefab);
        }
    }
}
