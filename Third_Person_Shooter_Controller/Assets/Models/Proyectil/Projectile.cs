using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 40f;
    public float lifeTime = 5f;
    public float damage = 25f;
    public bool usePhysics = false;

    [Header("Impact Effect")]
    public GameObject impactVfxPrefab;

    private Vector3 direction;
    private float timer;
    private Collider ownerCollider;

    // 🔹 Método público para inicializar el proyectil
    public void Fire(Vector3 dir, float spd = -1f, Collider owner = null)
    {
        direction = dir.normalized;
        if (spd > 0f) speed = spd;
        ownerCollider = owner;
        timer = lifeTime;

        // Evitar colisiones con el objeto que lo disparó
        Collider myCol = GetComponent<Collider>();
        if (ownerCollider != null && myCol != null)
            Physics.IgnoreCollision(myCol, ownerCollider, true);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        // Movimiento del proyectil
        if (!usePhysics)
        {
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = direction;
        }
    }

    // 🔹 Detectar impacto con enemigos u objetos
    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other == ownerCollider) return;
        if (other.CompareTag("Projectile")) return;

        // Detectar enemigos
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy == null)
            enemy = other.GetComponentInParent<EnemyHealth>();

        if (enemy != null)
            enemy.TakeDamage(damage);

        // Instanciar efecto visual en el punto de impacto
        if (impactVfxPrefab != null)
        {
            Instantiate(impactVfxPrefab, transform.position, Quaternion.LookRotation(direction));
        }

        Destroy(gameObject);
    }
}
