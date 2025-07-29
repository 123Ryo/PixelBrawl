using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public GameObject malePrefab;
    public GameObject femalePrefab;

    public static GameObject selectedCharacterPrefab;

    public void SelectMale()
    {
        selectedCharacterPrefab = malePrefab;
        SceneManager.LoadScene("MainScene");  //  遊戲主場景
    }

    public void SelectFemale()
    {
        selectedCharacterPrefab = femalePrefab;
        SceneManager.LoadScene("MainScene");
    }
}
