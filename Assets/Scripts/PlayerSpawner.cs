using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        if (CharacterSelector.selectedCharacterPrefab != null)
        {
            GameObject player = Instantiate(CharacterSelector.selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);

            // ✅ 自動指派給 CameraFollow 的 target
            CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
            if (camFollow != null)
            {
                camFollow.target = player.transform;
            }

            // ✅ 自動指派給 AttackController 的 playerTransform
            AttackController attackController = FindObjectOfType<AttackController>();
            if (attackController != null)
            {
                attackController.playerTransform = player.transform;
                attackController.attackRangeIndicator = player.transform.Find("AttackRangeIndicator")?.gameObject;
            }

            // ✅ 自動指派給所有敵人的 EnemyFollow playerTransform 欄位
            EnemyFollow[] enemies = FindObjectsOfType<EnemyFollow>();
            foreach (var enemy in enemies)
            {
                enemy.playerTransform = player.transform;
            }
        }
        else
        {
            Debug.LogWarning("未選擇角色！");
        }
    }
}
