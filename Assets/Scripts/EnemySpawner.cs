using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnTime = 2f;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
     private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        }
    }
}
