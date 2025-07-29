using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            agent.SetDestination(playerTransform.position);

            // 切換動畫
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }
}
