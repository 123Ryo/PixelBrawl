using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject characterSelectUI;

    void Start()
    {
        mainMenuUI.SetActive(true);         // 初始顯示封面
        characterSelectUI.SetActive(false); // 初始隱藏選角
    }

    public void ShowCharacterSelect()
    {
        mainMenuUI.SetActive(false);         // 隱藏封面
        characterSelectUI.SetActive(true);   // 顯示選角畫面
    }
}
