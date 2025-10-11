using UnityEngine;

public class RunnerSpeedCache : MonoBehaviour
{
    public Transform target;

    public static float ZDelta = 0f;

    private float lastZ = 0f;

    private void Start()
    {
        if (target != null)
        {
            lastZ = target.position.z;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            float z = target.position.z;
            ZDelta = z - lastZ;
            lastZ = z;
        }
    }
}
