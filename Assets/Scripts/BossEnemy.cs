using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletNormalSpeed = 20f;
    [SerializeField] private float bulletCircleSpeed = 10f;
    [SerializeField] private float hpValue = 100f;
    [SerializeField] private GameObject miniEnemy;
    [SerializeField] private float skillCooldown = 5f;
    private float nextSkillTime = 0f;
    [SerializeField] private GameObject usbPrefabs;
    protected override void Update()
    {
        base.Update();
        if (Time.time >= nextSkillTime)
        {
            {
                UseSkill();
            }
        }
    }

    protected override void Die()
    { 
        Instantiate(usbPrefabs, transform.position, Quaternion.identity);
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        { 
            player.TakeDamage(enterDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(stayDamage);
        }
    }

    private void ShotEnergy ()
    {
        if (player != null) 
        { 
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * bulletNormalSpeed);
        }
    }

    private void ShotCircleEnergy() 
    {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++) 
        {
            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(direction * bulletCircleSpeed);
        }
    }

    private void Heal(float hpAmount) 
    {
        currentHealth = Mathf.Min(currentHealth + hpAmount, maxHealth);
        UpdateHealthBar();
    }

    private void SpawnEnemy() 
    {
        Instantiate(miniEnemy, transform.position, Quaternion.identity);
    }

    private void Teleport() 
    {
        if (player != null) 
        {
            Vector3 randomPosition = player.transform.position + Random.insideUnitSphere * 5f;
            randomPosition.z = 0;
            transform.position = randomPosition;
        }
    }

    private void RandomSkill()
    {
        int randomSkill = Random.Range(0, 5);
        switch (randomSkill)
        {
            case 0:
                ShotEnergy();
                break;
            case 1:
                ShotCircleEnergy();
                break;
            case 2:
                Heal(20f);
                break;
            case 3:
                SpawnEnemy();
                break;
            case 4:
                Teleport();
                break;
        }
    }
    private void UseSkill() 
    {
        nextSkillTime = Time.time + skillCooldown;
        RandomSkill();
    }
}
