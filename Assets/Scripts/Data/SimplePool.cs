using System.Collections.Generic;
using UnityEngine;

public class SimplePool : MonoBehaviour
{
    [System.Serializable]
    public class PoolEntry
    {
        public string key;
        public GameObject prefab;
        public int initialSize = 4;
    }

    public List<PoolEntry> entries = new List<PoolEntry>();

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        for (int i = 0; i < entries.Count; ++i)
        {
            PoolEntry e = entries[i];

            if (pools.ContainsKey(e.key) == false)
            {
                pools[e.key] = new Queue<GameObject>();
            }

            for (int n = 0; n < e.initialSize; ++n)
            {
                GameObject go = Instantiate(e.prefab);
                go.SetActive(false);
                pools[e.key].Enqueue(go);
            }
        }
    }

    public GameObject Spawn(string key, Vector3 position, Quaternion rotation)
    {
        if (pools.ContainsKey(key) == false)
        {
            Debug.LogError($"Pool key not found: {key}");
            return null;
        }

        GameObject go = null;

        if (pools[key].Count > 0)
        {
            go = pools[key].Dequeue();
        }
        else
        {
            // 부족하면 동적으로 1개 더 생성
            PoolEntry e = entries.Find(x => x.key == key);

            if (e != null && e.prefab != null)
            {
                go = Instantiate(e.prefab);
            }
        }

        if (go != null)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
        }

        return go;
    }

    public void Despawn(string key, GameObject go)
    {
        if (go == null)
        {
            return;
        }

        go.SetActive(false);

        if (pools.ContainsKey(key) == false)
        {
            pools[key] = new Queue<GameObject>();
        }

        pools[key].Enqueue(go);
    }
}
