using UnityEngine;

public class SpinY : MonoBehaviour
{
    public float speed = 120f;

    private void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
}
