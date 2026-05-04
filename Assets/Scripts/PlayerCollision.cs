using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Player player = GetComponent<Player>();
            player.TakeDamage(10); // Adjust damage value as needed
            Destroy(collision.gameObject); // Destroy the bullet on impact
        }

        else if (collision.CompareTag("USB")) 
        {
            gameManager.WinGame();
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Energy")) 
        {
            gameManager.AddEnergy();
            Destroy(collision.gameObject);
            audioManager.PlayEnergySound();
        }

        else if (collision.CompareTag("Heart"))
        {
            player.Heal(20);
            Destroy(collision.gameObject);
        }    
    }
}
