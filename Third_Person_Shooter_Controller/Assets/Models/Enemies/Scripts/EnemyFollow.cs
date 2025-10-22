using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public float moveSpeed = 2f;
    public float stopDistance = 2f;

    private Transform player;

    private void Start()
    {
        // Busca al jugador por su tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("EnemyFollow: no se encontró ningún objeto con el tag 'Player'.");
    }

    private void Update()
    {
        if (player == null) return;

        // Calcula dirección hacia el jugador
        Vector3 dir = (player.position - transform.position);
        dir.y = 0f; // evita que suba o baje
        float dist = dir.magnitude;

        // Solo moverse si está lejos
        if (dist > stopDistance)
        {
            dir.Normalize();
            transform.position += dir * moveSpeed * Time.deltaTime;
        }

        // Rotar hacia el jugador suavemente
        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        }
    }
}
