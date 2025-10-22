using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int initialEnemyCount = 10;
    public int enemyIncrement = 5;
    public float initialSpeed = 2f;
    public float speedIncrement = 0.5f;
    public int totalWaves = 5;

    private int currentWave = 0;
    private int enemiesAlive = 0;

    void Update()
    {
        // Solo genera una nueva oleada si no hay enemigos vivos
        if (currentWave < totalWaves && enemiesAlive == 0)
        {
            SpawnWave();
            currentWave++;
        }
    }

    void SpawnWave()
    {
        int enemyCount = initialEnemyCount + (enemyIncrement * currentWave);
        float enemySpeed = initialSpeed + (speedIncrement * currentWave);

        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Ajustar velocidad del enemigo si tiene componente EnemyAI
            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.speed = enemySpeed;
                ai.waveManager = this; // Para que el enemigo pueda notificar su muerte
            }

            enemiesAlive++;
        }

        Debug.Log($"Oleada {currentWave + 1} generada con {enemyCount} enemigos a velocidad {enemySpeed}");
    }

    // Este método debe ser llamado por los enemigos al morir
    public void EnemyDied()
    {
        enemiesAlive--;
    }
}
