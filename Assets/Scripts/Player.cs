using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Image HealthBar;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameManager.PauseGame();
        }
    }
    void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed;
        if (playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (playerInput != Vector2.zero) {
            animator.SetBool("IsRun", true);
        }
        else {
            animator.SetBool("IsRun", false);
        }
    }

    public void TakeDamage(float damage)
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

    private void Die()
    {
        gameManager.GameOver();
    }

    public void Heal(float healAmount)
    {
        if (currentHealth < maxHealth) 
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            UpdateHealthBar();
        }
    }


}
