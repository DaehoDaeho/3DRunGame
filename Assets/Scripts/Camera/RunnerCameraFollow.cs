//using System.Numerics;
using UnityEngine;

public class RunnerCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.0f, 3.5f, -6.0f);
    public float followLerp = 6.0f;
    public float lookLerp = 10.0f;
        
    private void LateUpdate()
    {
        Vector3 wantedPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, wantedPos, followLerp * Time.deltaTime);

        Quaternion wantedRot = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRot, lookLerp * Time.deltaTime);
    }
}
