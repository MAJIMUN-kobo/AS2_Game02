using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform Player;
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

    // ===== 追加 =====
    [Header("スポナー情報")]
    public Vector3 spawnPoint;
    public float patrolRadius;          // ← スポナーから設定
    private bool returningToSpawn = false;
    // ===============

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();

        // ===== 追加 =====
        spawnPoint = transform.position;
        // ===============

        if (patrolPoints.Length > 0)
            agent.destination = patrolPoints[patrolIndex].position;
    }

    private void Update()
    {
        // ===== 追加 =====
        if (returningToSpawn)
        {
            if (agent.remainingDistance < 0.5f)
            {
                returningToSpawn = false;
                SetRandomPatrolAroundSpawn();
            }
            return;
        }
        // ===============

        if (!chasing)
        {
            if (PlayerInSight())
            {
                chasing = true;
            }

            if (agent.remainingDistance < 0.5f)
            {
                SetRandomPatrolAroundSpawn();
            }
        }
        else
        {
            float dist = Vector3.Distance(transform.position, Player.position);

            if (dist > stopDistance)
                agent.SetDestination(Player.position);
            else
                agent.SetDestination(transform.position);

            // ===== 追加 =====
            if (dist > loseDistance)
            {
                chasing = false;
                returningToSpawn = true;
                agent.SetDestination(spawnPoint);
            }
            // ===============
        }
    }

    bool PlayerInSight()
    {
        if (Player == null) return false;

        Vector3 eyePos = transform.position + Vector3.up * 1.5f;
        Vector3 dir = (Player.position + Vector3.up - eyePos).normalized;

        if (Vector3.Distance(eyePos, Player.position) > viewDistance) return false;
        if (Vector3.Angle(transform.forward, dir) > viewAngle / 2f) return false;

        if (Physics.Raycast(eyePos, dir, out RaycastHit hit, viewDistance))
        {
            if (hit.collider.CompareTag("Player"))
                return true;
        }

        return false;
    }

    // ===== 追加 =====
    void SetRandomPatrolAroundSpawn()
    {
        Vector3 randomPos = spawnPoint + Random.insideUnitSphere * patrolRadius;
        randomPos.y = spawnPoint.y;

        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
    // ===============
}
