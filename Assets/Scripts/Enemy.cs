using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 1;
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    [SerializeField] private Image HealthBar;

    [SerializeField] protected float enterDamage = 10f;
    [SerializeField] protected float stayDamage = 5f;

    protected Player player;

    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }

    protected void MoveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }

    protected void FlipEnemy()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void UpdateHealthBar()
    {
        if (HealthBar != null)
        {
            HealthBar.fillAmount = currentHealth / maxHealth;
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}