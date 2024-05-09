using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> PoolDictionaray;

    private void Awake()
    {
        PoolDictionaray = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            PoolDictionaray.Add(pool.tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!PoolDictionaray.ContainsKey(tag))
        {
            return null;
        }
        GameObject obj = PoolDictionaray[tag].Dequeue();
        PoolDictionaray[tag].Enqueue(obj);
        obj.SetActive(true);

        return obj;
    }
}
