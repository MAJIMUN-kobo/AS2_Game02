using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("出現させる敵")]
    public GameObject enemyPrefab;

    [Header("敵の行動範囲（スポナー中心）")]
    public float patrolRadius = 10f;

    [Header("プレイヤーが近づいたら出現する距離")]
    public float spawnDistance = 15f;

    [Header("内部参照（自動取得）")]
    private Transform player;

    [Header("出現済みかどうか")]
    private bool spawned = false;

    private void Start()
    {
        // Playerタグのオブジェクトを自動取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // すでに出現していたら何もしない
        if (spawned) return;

        // スポナーとプレイヤーの距離を測る
        float dist = Vector3.Distance(transform.position, player.position);

        // 一定距離内に入ったら敵を出現させる
        if (dist <= spawnDistance)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // 敵をスポナー位置に生成
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

        // 敵のAIを取得
        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        if (ai != null)
        {
            // パトロール半径をスポナーから渡す
            ai.patrolRadius = patrolRadius;

            // プレイヤー情報を敵に教える
            ai.Player = player;
        }

        // 二度とスポーンしないようにする
        spawned = true;
    }
}
