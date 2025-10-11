using UnityEngine;

public class SpinY : MonoBehaviour
{
    public float speed = 120f;

    private void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.World);
    }
}
