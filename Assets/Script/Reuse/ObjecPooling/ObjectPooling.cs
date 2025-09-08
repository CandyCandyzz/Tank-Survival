using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject objPrefabs;
    public int count;
}

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolsDict = new();

    private void Start()
    {
        SetPool();
    }

    private void SetPool()
    {
        foreach (var pool in pools)
        {
            Queue<GameObject> objsInPool = new();
            for (global::System.Int32 i = 0; i < pool.count; i++)
            {
                GameObject obj = Instantiate(pool.objPrefabs, transform);
                obj.SetActive(false);
                objsInPool.Enqueue(obj);
            }
            poolsDict.Add(pool.tag, objsInPool);
        }
    }

    public GameObject Get(string tag)
    {
        Queue<GameObject> objsInPool = poolsDict[tag];
        GameObject obj;
        if (objsInPool.Count > 0)
        {
            obj = objsInPool.Dequeue();
        }
        else
        {
            obj = Instantiate(pools.Find(pool => pool.tag == tag).objPrefabs, transform);
        }

        obj.SetActive(true);
        return obj;
    }

    public void Return(string tag, GameObject obj)
    {
        obj.SetActive(false);
        poolsDict[tag].Enqueue(obj);
    }    
}
