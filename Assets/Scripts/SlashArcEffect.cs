using UnityEngine;

public class SlashTrailMover : MonoBehaviour
{
    [Header("移動參數")]
    public float moveSpeed = 10f;
    public float duration = 0.2f;
    public Vector3 localMoveDirection = Vector3.forward;

    [Header("旋轉調整")]
    public Vector3 rotationOffset = Vector3.zero;

    [Header("縮放")]
    public Vector3 customScale = Vector3.one;

    private float timer;

    void Start()
    {
        // 設定初始旋轉
        transform.Rotate(rotationOffset);
        // 設定縮放
        transform.localScale = customScale;
    }

    void Update()
    {
        transform.Translate(localMoveDirection.normalized * moveSpeed * Time.deltaTime, Space.Self);
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
