using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // 重新載入目前遊戲場景（重新開始）
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // 回到主選單場景
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartMenu"); // 主選單場景
    }
}
