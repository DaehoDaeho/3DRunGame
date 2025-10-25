using UnityEngine;

public class RunnerCheckpointManager : MonoBehaviour
{
    public float lastCheckpointZ = 0f;    // 시작은 0
    public float respawnOffsetZ = 1.0f;   // 리스폰 시 약간 앞으로.
    public Vector3 respawnX_Y = new Vector3(0f, 0.0f, 0f); // X는 0(중앙), Y는 지면 높이 보정.

    public void SetCheckpoint(float z)
    {
        if (z > lastCheckpointZ)
        {
            lastCheckpointZ = z;
        }
    }

    // 리스폰 처리: 위치 이동 + 수직속도 초기화.
    public void Respawn(CharacterController cc, Transform playerTransform)
    {
        float targetZ = lastCheckpointZ + respawnOffsetZ;
        Vector3 pos = playerTransform.position;
        pos.x = 0f;
        pos.y = 0.1f;
        pos.z = targetZ;

        if (cc != null)
        {
            // CC는 직접 위치 세팅이 안 되고 Move를 써야 안전한 편이라 두 단계 처리.
            cc.enabled = false;
            playerTransform.position = pos;
            cc.enabled = true;
        }
        else
        {
            playerTransform.position = pos;
        }
    }
}
