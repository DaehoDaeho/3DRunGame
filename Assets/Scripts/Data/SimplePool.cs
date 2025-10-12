using System.Collections.Generic;
using UnityEngine;

public class SimplePool : MonoBehaviour
{
    [System.Serializable]
    public class PoolEntry  // 맵 정보를 담을 클래스.
    {
        public string key;  // 맵 이름.
        public GameObject prefab;   // 맵 프리팹.
        public int initialSize = 4; // 미리 만들어 놓을 맵의 개수.
    }

    public List<PoolEntry> entries = new List<PoolEntry>(); // 맵 프리팹 정보를 저장할 리스트.

    // 미리 생성한 맵을 보관할 보관함.
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        // 맵의 종류의 개수만큼 반복문을 돌면서 맵을 생성.
        for (int i = 0; i < entries.Count; ++i)
        {
            PoolEntry e = entries[i];

            if (pools.ContainsKey(e.key) == false)
            {
                pools[e.key] = new Queue<GameObject>();
            }

            // 각 맵마다 미리 만들어 놓을 개수만큼 반복문을 돌면서 맵을 생성.
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

        if (pools[key].Count > 0)   // 사용가능한 오브젝트가 남아 있으면 가져온다.
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
