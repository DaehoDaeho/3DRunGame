using UnityEngine;

public class TileCoinRow : MonoBehaviour
{
    public SimplePool pool;
    public string coinKey = "Coin";

    public int count = 5;   // 몇 개 뿌릴지.
    public float startZ = 4f;   // 타일 내부 시작 Z
    public float stepZ = 2.5f;  // 간격.
    public int laneIndex = 0;   // -1, 0, +1 (레인 선택)
    public float laneOffset = 2f;   // 레인 간 X 거리.

    private GameObject[] spawned;

    private void OnEnable()
    {
        // 타일이 스폰될 때 호출됨
        if (pool == null)
        {
            pool = FindAnyObjectByType<SimplePool>();
        }

        if (spawned == null || spawned.Length != count)
        {
            spawned = new GameObject[count];
        }

        float x = laneIndex * laneOffset;

        for (int i = 0; i < count; i = i + 1)
        {
            float zLocal = startZ + (i * stepZ);
            Vector3 worldPos = transform.position + new Vector3(x, 1.0f, zLocal);
            GameObject c = pool.Spawn(coinKey, worldPos, Quaternion.identity);
            spawned[i] = c;
        }
    }

    private void OnDisable()
    {
        // 타일이 반환될 때 코인도 함께 반환.
        if (spawned == null)
        {
            return;
        }

        for (int i = 0; i < spawned.Length; i = i + 1)
        {
            GameObject c = spawned[i];

            if (c != null)
            {
                SimplePool p = (pool != null) ? pool : FindAnyObjectByType<SimplePool>();

                if (p != null)
                {
                    p.Despawn(coinKey, c);
                }
                else
                {
                    c.SetActive(false);
                }
            }
        }
    }
}
