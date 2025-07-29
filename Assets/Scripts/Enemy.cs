using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;  // 新增欄位，記得在 Inspector 連結你的血條物件

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} 受到 {amount} 傷害，剩下 {currentHealth}");

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} 死亡！");
        Destroy(gameObject);
    }
}
