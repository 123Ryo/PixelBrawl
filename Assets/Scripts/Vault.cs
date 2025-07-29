using UnityEngine;
using UnityEngine.UI;

public class Vault : MonoBehaviour
{
    public float maxHealth = 1000f;
    private float currentHealth;

    public HealthBar healthBar; // 拖進 UI 血條元件

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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
        Debug.Log("💥 金庫被摧毀！");
        // 可以加：顯示 Game Over、停止遊戲等
    }
}
