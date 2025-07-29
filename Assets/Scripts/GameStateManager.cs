using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject winText;
    public GameObject loseText;
    public GameObject endPanel;
    public GameObject vault;
    public float checkInterval = 1f;

    private bool gameEnded = false;

    void Start()
    {
        Time.timeScale = 1f; // ✅ 開始遊戲時確保遊戲為正常速度
        endPanel.SetActive(false);
        winText.SetActive(false);
        loseText.SetActive(false);

        InvokeRepeating(nameof(CheckGameState), 1f, checkInterval);
    }

    void CheckGameState()
    {
        if (gameEnded) return;

        bool hasWon = false;
        bool hasLost = false;

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
            hasWon = true;

        if (vault != null)
        {
            Vault vaultScript = vault.GetComponent<Vault>();
            if (vaultScript != null)
            {
                float vaultHealth = GetVaultHealth(vaultScript);
                if (vaultHealth <= 0)
                    hasLost = true;
            }
        }

        // ✅ 防止同時觸發
        if (hasWon && !hasLost)
        {
            WinGame();
        }
        else if (hasLost && !hasWon)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        gameEnded = true;
        winText.SetActive(true);
        loseText.SetActive(false);  // ✅ 確保另一個隱藏
        endPanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("🎉 玩家通關！");
    }

    void LoseGame()
    {
        gameEnded = true;
        loseText.SetActive(true);
        winText.SetActive(false);  // ✅ 確保另一個隱藏
        endPanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("💀 遊戲失敗！");
    }

    private float GetVaultHealth(Vault vault)
    {
        var field = typeof(Vault).GetField("currentHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (float)(field?.GetValue(vault) ?? 0f);
    }
}
