using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;

    [Header("虛擬搖桿（可選）")]
    public FixedJoystick moveJoystick; // 新增虛擬搖桿參考

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
        //  移動輸入處理：搖桿優先，若無搖桿則使用鍵盤
        float moveX = 0f;
        float moveZ = 0f;

        if (moveJoystick != null)
        {
            //  使用虛擬搖桿輸入（手機版）
            moveX = moveJoystick.Horizontal;
            moveZ = moveJoystick.Vertical;
        }
        else
        {
            //  鍵盤輸入（電腦版）
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);

        // 🎞 設定動畫速度
        animator.SetFloat("Speed", move.magnitude);

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
