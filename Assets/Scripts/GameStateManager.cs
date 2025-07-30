using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject winText;
    public GameObject loseText;
    public GameObject endPanel;
    public GameObject vault;
    public GameObject player; //  指定玩家物件
    public float checkInterval = 1f;

    private bool gameEnded = false;

    void Start()
    {
        Time.timeScale = 1f; //  開始遊戲時確保遊戲為正常速度
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

        //  判斷是否擊敗所有敵人
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
            hasWon = true;

        //  判斷金庫是否被摧毀
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

        //  判斷玩家是否死亡
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                float playerHealth = GetPlayerHealth(playerController);
                if (playerHealth <= 0)
                    hasLost = true;
            }
        }

        //  判斷輸贏邏輯（防止同時觸發）
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
        loseText.SetActive(false);  //  確保另一個隱藏
        endPanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("🎉 玩家通關！");
    }

    void LoseGame()
    {
        gameEnded = true;
        loseText.SetActive(true);
        winText.SetActive(false);  //  確保另一個隱藏
        endPanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("💀 遊戲失敗！");
    }

    private float GetVaultHealth(Vault vault)
    {
        var field = typeof(Vault).GetField("currentHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (float)(field?.GetValue(vault) ?? 0f);
    }

    // 讀取 PlayerController 中的 private currentHealth
    private float GetPlayerHealth(PlayerController player)
    {
        var field = typeof(PlayerController).GetField("currentHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (float)(field?.GetValue(player) ?? 0f);
    }
}
