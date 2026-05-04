using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject bloodPrefabs;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        MoveBullet();
    }
    void MoveBullet()
    {         
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                GameObject bloodEffect = Instantiate(bloodPrefabs, transform.position, Quaternion.identity);
                Destroy(bloodEffect, 1f);
            }
            Destroy(gameObject);
        }
    }
}
