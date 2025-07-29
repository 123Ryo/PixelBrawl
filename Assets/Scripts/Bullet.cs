using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 50;
    public float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // 幾秒後自動銷毀
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            // 子彈打到牆壁就消失
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TakeDamage(damage);
                Debug.Log("💥 子彈擊中玩家！");
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Vault"))
        {
            Vault vault = other.GetComponent<Vault>();
            if (vault != null)
            {
                vault.TakeDamage(damage);
                Debug.Log("🏦 子彈擊中金庫！");
            }

            Destroy(gameObject);
        }
    }
}
