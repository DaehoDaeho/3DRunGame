using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;
    public SimplePool pool; 

    [Header("Tile Keys (Pool keys)")]    
    public string[] tileKeys;

    [Header("Config")]
    public float tileLength = 20f;  // 각 타일 길이(20m)
    public int keepTiles = 7;   // 유지할 타일 수(화면 앞뒤 포함)
    public int startTiles = 5;  // 시작 시 깔아둘 타일 수.

    private LinkedList<GameObject> activeTiles = new LinkedList<GameObject>();
    private float nextZ = 0f;   // 다음 타일이 시작될 Z

    private void Start()
    {
        // 시작 타일 깔기: 직선 3 + 랜덤 2
        for (int i = 0; i < startTiles; ++i)
        {
            string key = tileKeys[0];

            if (i >= 3)
            {
                key = RandomKey();
            }

            SpawnTile(key);
        }
    }

    private void Update()
    {
        // 플레이어가 너무 앞서가면, 뒤 타일은 반환하고 앞에 새 타일 추가.
        float playerZ = player.position.z;
        float firstTileStartZ = 0f;

        if (activeTiles.First != null)
        {
            firstTileStartZ = activeTiles.First.Value.transform.position.z;
        }

        // 첫 타일의 끝이 플레이어 뒤로 충분히 지나갔으면 반환.
        while (activeTiles.Count > 0)
        {
            GameObject first = activeTiles.First.Value;
            float startZ = first.transform.position.z;
            float endZ = startZ + tileLength;

            if (endZ < playerZ - tileLength)
            {
                // 반환.
                ReturnTile(first);
                activeTiles.RemoveFirst();
            }
            else
            {
                break;
            }
        }

        // 유지 개수보다 적으면 채움.
        while (activeTiles.Count < keepTiles)
        {
            SpawnTile(RandomKey());
        }
    }

    private string RandomKey()
    {
        int r = Random.Range(0, tileKeys.Length);

        return tileKeys[r];
    }

    private void SpawnTile(string key)
    {
        // 타일 프리팹의 원점이 시작점이므로, Z=nextZ 에 위치.
        Vector3 pos = new Vector3(0f, 0f, nextZ);
        GameObject tile = pool.Spawn(key, pos, Quaternion.identity);

        if (tile != null)
        {
            activeTiles.AddLast(tile);
            nextZ = nextZ + tileLength;

            // 타일에 '자기 자신을 반환'할 장치가 있으면 연결한다.
            TileReturn tr = tile.GetComponent<TileReturn>();

            if (tr != null)
            {
                tr.spawner = this;
                tr.poolKey = key;
            }
        }
    }

    private void ReturnTile(GameObject tile)
    {
        TileReturn tr = tile.GetComponent<TileReturn>();

        if (tr != null)
        {
            pool.Despawn(tr.poolKey, tile);
        }
        else
        {
            // 키를 모르면 기본 스트레이트로 반환해도 무방.
            pool.Despawn(tileKeys[0], tile);
        }
    }
}
