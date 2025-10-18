using UnityEngine;

public class TileCoinRow : MonoBehaviour
{
    public SimplePool pool;
    public string coinKey = "Coin";

    public int count = 5;
    public float startZ = 4f;
    public float stepZ = 2.5f;

    public int laneIndex = 0;   // -1, 0, +1
    public float laneOffset = 2f;   // ∑π¿Œ ∞£ X

    private GameObject[] spawned;

    private void OnEnable()
    {
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
