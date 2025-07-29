using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        // 載入你主遊戲的場景名稱
        SceneManager.LoadScene("MainScene");
    }
}
