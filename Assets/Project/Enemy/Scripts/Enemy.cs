using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform player;                 
    public LayerMask obstacleMask;

    [Header("追跡設定")]
    public bool chasing;
    public float chaseDistance = 10f;
    public float loseDistance = 15f;
    public float stopDistance = 4f;

    [Header("視界設定")]
    public float viewAngle = 60f;
    public float viewDistance = 10f;

    [Header("巡回設定")]
    public NavMeshAgent agent;
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    public float memoryTime = 5f;
    private float memoryCounter = 0f;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
            agent.destination = patrolPoints[patrolIndex].position;
    }

    private void Update()
    {
        if (!chasing)
        {
            // 視界にプレイヤーが入ったら追跡
            if (PlayerInSight())
            {
                chasing = true;
            }

            // 追跡解除後の記憶カウント
            if (memoryCounter > 0)
            {
                memoryCounter -= Time.deltaTime;
                if (memoryCounter <= 0)
                {
                    NextPatrolPoint();
                }
            }

            // パトロール続行
            if (agent.remainingDistance < 0.5f)
            {
                NextPatrolPoint();
            }
        }
        else
        {
            // 追跡中
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist > stopDistance)
                agent.SetDestination(player.position); // プレイヤーに向かう
            else
                agent.SetDestination(transform.position); // 目の前なら停止

            // 視界外 or 距離オーバー → 追跡解除
            if (!PlayerInSight() || dist > loseDistance)
            {
                chasing = false;
                memoryCounter = memoryTime;
            }
        }
    }

    /// <summary>
    /// プレイヤーが視界内にいるか判定
    /// </summary>
    bool PlayerInSight()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        // 距離
        if (Vector3.Distance(transform.position, player.position) > viewDistance)
            return false;

        // 視野角
        if (Vector3.Angle(transform.forward, dir) > viewAngle / 2f)
            return false;

        // Raycast（壁があれば見えない）
        if (Physics.Raycast(transform.position + Vector3.up * 1f, dir, out RaycastHit hit, viewDistance, ~obstacleMask))
        {
            if (hit.collider.CompareTag("Player"))
                return true;
        }

        return false;
    }

    /// <summary>
    /// 次のパトロール地点へ
    /// </summary>
    void NextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        patrolIndex = Random.Range(0, patrolPoints.Length);
        agent.destination = patrolPoints[patrolIndex].position;
    }
}
