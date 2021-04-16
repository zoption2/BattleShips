using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Pool : Singleton<Pool>
{
    #region Awake
    private void Awake()
    {
        //Instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        pool = new GameObject("TotalPool");

        //foreach (ToSpawn spawner in spawners)
        //{
        //    AddToSpawner(spawner.tag, spawner.prefab, spawner.size);
        //}
    }
    #endregion

    [System.Serializable]
    public class ToSpawn
    {
        public string tag;
        public GameObject prefab;
        public int size;


        public ToSpawn(string tagOfSpawnObject, GameObject prefabOfObject, int sizeOfPool)
        {
            this.tag = tagOfSpawnObject;
            this.prefab = prefabOfObject;
            this.size = sizeOfPool;
        }
    }

    public List<ToSpawn> spawners;

    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private GameObject pool;
    private PrefabHolder prefabHolder;



    public async void AddToPool(string whatToAdd, int count, string newTag)
    {
        if(!poolDictionary.ContainsKey(newTag))
        {
            GameObject newOne = new GameObject("Pool" + newTag);
            newOne.transform.SetParent(pool.transform);
            Queue<GameObject> objectSpawner = new Queue<GameObject>();

            await PrefabHolder.Instance.LoadPrefab(whatToAdd);
            GameObject currentPrefab = PrefabHolder.Instance.GetCurrentPrefab();

            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(currentPrefab, newOne.transform);
                obj.SetActive(false);
                objectSpawner.Enqueue(obj);
            }

            poolDictionary.Add(newTag, objectSpawner);
        }
        else
        {
            Debug.LogWarning("Pool alredy contain key: " + newTag);
        }
    }



    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Спаунера " + tag + " не существует!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
