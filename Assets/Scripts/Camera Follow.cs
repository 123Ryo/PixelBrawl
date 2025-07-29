using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // 要跟隨的物件（角色）
    public Vector3 offset = new Vector3(0, 10, -6); // 從角色上方斜後方觀看
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(target); // 相機永遠看著角色
    }
}
