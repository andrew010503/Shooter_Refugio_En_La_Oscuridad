using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Death FX (Opcional)")]
    [SerializeField] private GameObject deathEffect;   // Partículas o efecto visual al morir
    [SerializeField] private AudioClip deathSound;     // Sonido opcional de muerte
    [SerializeField] private float destroyDelay = 0.3f; // Tiempo antes de eliminar el enemigo

    // Evento que avisa al EnemySpawner que este enemigo murió
    public event Action OnDeath;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return; // evita múltiples muertes

        currentHealth -= amount;

        if (currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // 🔥 Efecto de muerte (si existe)
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position + Vector3.up * 1f, Quaternion.identity);
        }

        // 🎵 Sonido opcional
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        // ⚙️ Avisar al EnemySpawner (evento)
        OnDeath?.Invoke();

        // 💀 Destruir el objeto después de un pequeño retraso
        Destroy(gameObject, destroyDelay);
    }
}
