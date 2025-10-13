using UnityEngine;
using UnityEngine.AI;

public class Chasing : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] GameObject player;
    [SerializeField] private float speed = 2.5f;

    public bool isChasing  = false;
    private bool collidedWithPlayer = false;
    private void Awake()
    {
        if (navMeshAgent == null) navMeshAgent = GetComponent<NavMeshAgent>();
        if (player == null) player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        if (animator) animator.SetBool("IsChasing", false);
    }

    void Update()
    {
        if(isChasing && !collidedWithPlayer)
        {
            if (animator) animator.SetBool("IsChasing", true);
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.speed = speed;
        animator.SetFloat("speed", speed);
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.name = player.name;
        isChasing = false;
        collidedWithPlayer = true;
    }
}
