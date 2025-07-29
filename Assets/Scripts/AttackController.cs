using UnityEngine; 
using UnityEngine.EventSystems;

public class AttackController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Joystick 與玩家")]
    public FixedJoystick attackJoystick;
    public Transform playerTransform;

    [Header("攻擊範圍顯示物件")]
    public GameObject attackRangeIndicator;

    [Header("攻擊設定")]
    public float attackRadius = 2f;
    public float attackRangeOffset = 1.5f;
    public float attackAngle = 60f;
    public int attackDamage = 50;
    public LayerMask enemyLayerMask;

    [Header("攻擊音效設定")]
    public AudioClip attackSound;  // ← ✅ 新增音效檔
    private AudioSource audioSource;  // ← ✅ 播放器

    [Header("攻擊特效（Prefab）")]
    public GameObject slashEffectPrefab;  // ✅ 攻擊時的斬擊特效

    private bool isHolding = false;
    private Animator playerAnimator;
    private Vector3 lastDirection = Vector3.forward;

    void Start()
    {
        if (attackRangeIndicator != null)
        {
            attackRangeIndicator.SetActive(false);
            Debug.Log("✅ 攻擊範圍初始關閉成功！");
        }

        if (playerTransform != null)
            playerAnimator = playerTransform.GetComponent<Animator>();

        // ✅ 初始化音效播放器
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.volume = 0.5f; // 🔊 設定音量為 50%，數值範圍是 0.0 ~ 1.0
    }

    void Update()
    {
        if (isHolding)
        {
            Vector2 input = new Vector2(attackJoystick.Horizontal, attackJoystick.Vertical);

            if (input.magnitude > 0.1f)
            {
                lastDirection = new Vector3(input.x, 0, input.y).normalized;
                playerTransform.forward = lastDirection;

                if (attackRangeIndicator != null)
                {
                    Vector3 offset = lastDirection * attackRangeOffset + Vector3.up * 0.01f;
                    attackRangeIndicator.transform.position = playerTransform.position + offset;
                    attackRangeIndicator.SetActive(true);
                }
            }
        }
    }

    public void TriggerPointerDown() => OnPointerDown(null);
    public void TriggerPointerUp() => OnPointerUp(null);

    public void OnPointerDown(PointerEventData e)
    {
        isHolding = true;
        if (attackRangeIndicator != null)
            attackRangeIndicator.SetActive(true);
    }

    public void OnPointerUp(PointerEventData e)
    {
        isHolding = false;
        if (attackRangeIndicator != null)
            attackRangeIndicator.SetActive(false);

        PerformAttack(lastDirection);
    }

    private void PerformAttack(Vector3 dir)
    {
        // ✅ 若 Animator 尚未初始化，則自動補抓一次
        if (playerAnimator == null && playerTransform != null)
        {
            playerAnimator = playerTransform.GetComponentInChildren<Animator>();
            if (playerAnimator != null)
                Debug.Log("✅ 自動補抓 Animator 成功！");
            else
                Debug.LogWarning("⚠️ 無法找到 Animator，請檢查角色上是否掛有 Animator！");
        }

        Debug.Log("🎯 執行攻擊！方向：" + dir);
        playerAnimator?.SetTrigger("Attack");

        // ✅ 播放音效（只要有音效檔）
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

        // ✅ 產生斬擊特效
        if (slashEffectPrefab != null && playerTransform != null)
        {
            Vector3 spawnPos = playerTransform.position + dir * 1f + Vector3.up * 1f;
            Quaternion spawnRot = Quaternion.LookRotation(dir);
            GameObject fx = Instantiate(slashEffectPrefab, spawnPos, spawnRot);
            Destroy(fx, 1f); // 自動清除
        }

        Vector3 attackCenter = playerTransform.position + dir * attackRangeOffset;
        Collider[] hitColliders = Physics.OverlapSphere(attackCenter, attackRadius, enemyLayerMask);

        foreach (var col in hitColliders)
        {
            Vector3 toTarget = (col.transform.position - attackCenter).normalized;
            if (Vector3.Angle(dir, toTarget) <= attackAngle / 2f)
            {
                Debug.Log($"💥 擊中敵人：{col.name}");
                col.GetComponent<Enemy>()?.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (playerTransform == null) return;

        Vector3 dir = lastDirection.normalized;
        Vector3 center = playerTransform.position + dir * attackRangeOffset;

        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);

        Quaternion leftRot = Quaternion.AngleAxis(-attackAngle / 2f, Vector3.up);
        Quaternion rightRot = Quaternion.AngleAxis(attackAngle / 2f, Vector3.up);
        Vector3 leftDir = leftRot * dir;
        Vector3 rightDir = rightRot * dir;

        Gizmos.DrawLine(center, center + leftDir * attackRadius);
        Gizmos.DrawLine(center, center + rightDir * attackRadius);

        int segmentCount = 30;
        float angleStep = attackAngle / segmentCount;
        Vector3 prevPoint = center + leftDir * attackRadius;
        for (int i = 1; i <= segmentCount; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(-attackAngle / 2f + angleStep * i, Vector3.up);
            Vector3 currDir = rot * dir;
            Vector3 currPoint = center + currDir * attackRadius;
            Gizmos.DrawLine(prevPoint, currPoint);
            prevPoint = currPoint;
        }
    }
}
