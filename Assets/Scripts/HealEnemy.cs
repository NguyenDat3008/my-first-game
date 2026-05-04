using UnityEngine;

public class HealEnemy : Enemy
{
    [SerializeField] private float healAmount = 20f;
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

    private void HealPlayer()
    {
        if (player != null)
        {
            player.Heal(healAmount);
        }
    }
    protected override void Die()
    {
        base.Die();
        HealPlayer();
    }
}
