using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        if (CharacterSelector.selectedCharacterPrefab != null)
        {
            GameObject player = Instantiate(CharacterSelector.selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);

            // 自動指派給 CameraFollow 的 target
            CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
            if (camFollow != null)
            {
                camFollow.target = player.transform;
            }

            // 自動指派給 AttackController 的 playerTransform
            AttackController attackController = FindObjectOfType<AttackController>();
            if (attackController != null)
            {
                attackController.playerTransform = player.transform;
                attackController.attackRangeIndicator = player.transform.Find("AttackRangeIndicator")?.gameObject;
            }

            // 自動指派給所有敵人的 EnemyFollow playerTransform 欄位
            EnemyFollow[] enemies = FindObjectsOfType<EnemyFollow>();
            foreach (var enemy in enemies)
            {
                enemy.playerTransform = player.transform;
            }

            // 將生成的玩家指定給 GameStateManager 的 Player 欄位
            GameStateManager gameStateManager = FindObjectOfType<GameStateManager>();
            if (gameStateManager != null)
            {
                gameStateManager.player = player;
            }

            // 自動指派移動搖桿（MoveJoystick）
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                FixedJoystick moveJoystick = GameObject.Find("MoveJoystick")?.GetComponent<FixedJoystick>();
                if (moveJoystick != null)
                {
                    playerController.moveJoystick = moveJoystick;
                    Debug.Log("✅ 成功指派移動搖桿給 PlayerController！");
                }
                else
                {
                    Debug.LogWarning("⚠️ 找不到 MoveJoystick，請確認場景中有命名為 MoveJoystick 的物件！");
                }
            }
        }
        else
        {
            Debug.LogWarning("未選擇角色！");
        }
    }
}
