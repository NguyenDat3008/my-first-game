using UnityEngine;

public class ExplosionEnemy : Enemy
{
    [SerializeField] private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        if (explosionPrefab != null) 
        { 
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
    protected override void Die()
    {
        Explode();
        base.Die();
    }

}
