using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public int health = 3;
    private Transform player;
    private NavMeshAgent agent;

    // Referencia al WaveManager
    public WaveManager waveManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (player != null)
            agent.SetDestination(player.position);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Notificar al WaveManager antes de destruir el enemigo
            if (waveManager != null)
            {
                waveManager.EnemyDied();
            }

            Destroy(gameObject);
        }
    }
}

