using UnityEngine;

public class HealEnemy : Enemy
{
    [SerializeField] private GameObject heartObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(enterDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(stayDamage * Time.deltaTime);
        }
    }

    protected override void Die()
    {
        if (heartObject != null)
        {
            GameObject heart = Instantiate(heartObject, transform.position, Quaternion.identity);
        }
        base.Die();
    }
}
