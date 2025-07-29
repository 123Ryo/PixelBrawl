using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;

    [Header("血量設定")]
    public float maxHealth = 1000f;
    private float currentHealth;

    public HealthBar healthBar;

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);

        animator.SetFloat("Speed", move.magnitude);

        // ✅ ❌ 這段移除（避免搶走攻擊方向控制）
        // if (move != Vector3.zero)
        // {
        //     transform.forward = move;
        // }

        // 測試動畫（H 鍵）
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayAttackAnimation();
        }

        // 測試扣血（J 鍵）
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(100f);
        }
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
    }
}
