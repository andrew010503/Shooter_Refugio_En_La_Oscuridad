using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnHeight = 0f;

    [Header("Wave Settings")]
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveDifficultyMultiplier = 1.2f;
    [SerializeField] private int maxWaves = 99;

    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(WaveController());
    }

    private IEnumerator WaveController()
    {
        yield return new WaitForSeconds(1f);

        while (currentWave < maxWaves)
        {
            // Espera hasta que no haya enemigos
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            yield return new WaitForSeconds(timeBetweenWaves);

            StartWave();
        }
    }

    private void StartWave()
    {
        currentWave++;
        Debug.Log($"🌀 Iniciando oleada {currentWave}");

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            pos.y = spawnHeight;

            GameObject newEnemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
            activeEnemies.Add(newEnemy);

            // Captura local para evitar problemas de cierre
            EnemyHealth health = newEnemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.OnDeath += () =>
                {
                    activeEnemies.Remove(newEnemy);
                    Debug.Log($"💀 Enemigo eliminado. Restan {activeEnemies.Count}");

                    // Limpia referencias nulas
                    activeEnemies.RemoveAll(e => e == null);
                };
            }
            else
            {
                Debug.LogWarning("⚠️ El enemigo instanciado no tiene EnemyHealth adjunto.");
            }
        }

        // Incrementa la dificultad poco a poco
        enemiesPerWave = Mathf.CeilToInt(enemiesPerWave * waveDifficultyMultiplier);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
