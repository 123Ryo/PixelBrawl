using UnityEngine;
using UnityEngine.UI;
using TMPro; //  TextMeshProUGUI

public class HealthBar : MonoBehaviour
{
    [Header("血條元件")]
    public Image fillImage;                // 指向 HealthBarFill（Image）
    public TextMeshProUGUI healthText;    // 指向 HealthText（TextMeshProUGUI）

    [Header("跟隨目標")]
    public Transform followTarget;        // 角色頭上空物件（如 HealthBarAnchor）
    public Vector3 offset = new Vector3(0, 2f, 0); // 血條高度偏移

    private float currentHealth;
    private float maxHealth;

    void Update()
    {
        // 讓血條面向攝影機
        if (Camera.main != null)
            transform.forward = Camera.main.transform.forward;

        // 跟隨角色頭部
        if (followTarget != null)
            transform.position = followTarget.position + offset;
    }

    /// <summary>
    /// 設定最大血量
    /// </summary>
    public void SetMaxHealth(float max)
    {
        maxHealth = max;
        currentHealth = max;
        UpdateUI();
    }

    /// <summary>
    /// 設定目前血量
    /// </summary>
    public void SetHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        UpdateUI();
    }

    /// <summary>
    /// 更新血條圖和文字
    /// </summary>
    private void UpdateUI()
    {
        if (fillImage != null)
            fillImage.fillAmount = maxHealth > 0 ? currentHealth / maxHealth : 0;

        if (healthText != null)
            healthText.text = Mathf.CeilToInt(currentHealth).ToString(); // 顯示整數
    }
}
