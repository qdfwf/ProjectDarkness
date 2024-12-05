using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;  // Радиус обнаружения игрока
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

   void Update()
{
    float distanceToPlayer = Vector3.Distance(player.position, transform.position);

    if (distanceToPlayer <= detectionRange)
    {
        agent.SetDestination(player.position);
        
        if (agent.velocity.sqrMagnitude > 0.1f) // Проверяем, движется ли агент
        {
            animator.SetBool("isMoving", true); // Включаем анимацию движения
        }
        else
        {
            animator.SetBool("isMoving", false); // Включаем анимацию ожидания
        }
    }
    else
    {
        agent.ResetPath();
        animator.SetBool("isMoving", false); // Включаем анимацию ожидания
    }
}

}
