using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 控制 Idle / Run
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }
}
