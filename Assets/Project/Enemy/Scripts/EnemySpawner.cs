using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float patrolRadius = 10f;
    public float spawnDistance = 15f;

    private Transform player;
    private bool spawned = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (spawned) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= spawnDistance)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.patrolRadius = patrolRadius;   // ÅöÇ±Ç±Ç≈ê›íË
            ai.Player = player;
        }

        spawned = true;
    }
}
