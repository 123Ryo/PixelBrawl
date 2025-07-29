using UnityEngine; 
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Transform vault;

    [Header("移動設定")]
    public float chaseDistance = 10f;
    public float stopDistance = 7f;
    public float moveSpeed = 3f;

    [Header("攻擊設定")]
    public float attackRange = 10f;
    public float attackCooldown = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;

    [Header("攻擊音效")]
    public AudioClip attackSound;               // 攻擊時播放的音效
    private AudioSource audioSource;            // 音效播放元件

    private NavMeshAgent agent;
    private Animator animator;

    private float nextAttackTime = 0f;
    private Transform currentTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = moveSpeed;

        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        if (vault == null)
            vault = GameObject.FindWithTag("Vault")?.transform;

        // 取得或新增 AudioSource 組件
        audioSource = GetComponent<AudioSource>();
if (audioSource == null)
    audioSource = gameObject.AddComponent<AudioSource>();

audioSource.volume = 0.2f; // 🔊 設定音量為 50%，數值範圍是 0.0 ~ 1.0

    }

    void Update()
    {
        if (vault == null) return;

        float playerDist = player != null ? Vector3.Distance(transform.position, player.position) : Mathf.Infinity;
        float vaultDist = Vector3.Distance(transform.position, vault.position);

        bool canSeePlayer = player != null && HasLineOfSight(player);
        bool canSeeVault = HasLineOfSight(vault);

        // 優先攻擊金庫（在範圍內 & 看得到）
        if (vaultDist <= attackRange && canSeeVault)
        {
            currentTarget = vault;
            ActAsCombatTarget(vault, vaultDist);
        }
        // 攻擊玩家（在範圍內 & 看得到）
        else if (player != null && playerDist <= attackRange && canSeePlayer)
        {
            currentTarget = player;
            ActAsCombatTarget(player, playerDist);
        }
        // 追蹤玩家（在偵測範圍內 & 看得到）
        else if (player != null && playerDist <= chaseDistance && canSeePlayer)
        {
            currentTarget = player;
            ActAsChaser(player);
        }
        // 其他情況 → 追金庫
        else
        {
            currentTarget = vault;
            ActAsChaser(vault);
        }
    }

    /// <summary>
    /// 判斷 firePoint 到目標之間是否有牆擋住（用 Raycast）
    /// </summary>
    bool HasLineOfSight(Transform target)
    {
        Vector3 dir = (target.position - firePoint.position).normalized;
        if (Physics.Raycast(firePoint.position, dir, out RaycastHit hit, attackRange))
        {
            return hit.transform == target;
        }
        return false;
    }

    void ActAsCombatTarget(Transform target, float distance)
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void ActAsChaser(Transform target)
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Attack()
    {
        Debug.Log("敵人觸發 Attack()！看秒數：" + Time.time);
        animator.SetTrigger("Attack");

        // 播放攻擊音效
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

        // 生成子彈
        if (bulletPrefab != null && firePoint != null)
        {
            Debug.Log("嘗試生成子彈");

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }
            else
            {
                Debug.LogWarning("子彈上沒有 Rigidbody！");
            }
        }
        else
        {
            Debug.LogWarning("bulletPrefab 或 firePoint 尚未設定！");
        }
    }
}
