using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform Player;
    public Transform Item;
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

    // ===== スポナー関連 =====
    [Header("スポナー情報")]
    public Vector3 spawnPoint;
    public float patrolRadius = 10f;
    private bool returningToSpawn = false;

    // ===== おとり関連 =====
    [Header("おとり設定")]
    public float lureStopTime = 3f;
    private bool lured = false;
    private float lureTimer = 0f;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();

        if (Player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) Player = p.transform;
        }

        spawnPoint = transform.position;
        SetRandomPatrolAroundSpawn();
    }

    private void Update()
    {
        // ===== おとり中 =====
        if (lured)
        {
            lureTimer -= Time.deltaTime;
            agent.SetDestination(transform.position);

            if (lureTimer <= 0f)
            {
                lured = false;
                returningToSpawn = true;
                agent.SetDestination(spawnPoint);
            }
            return;
        }

        // ===== スポナーへ帰還 =====
        if (returningToSpawn)
        {
            if (PlayerInSight())
            {
                chasing = true;
                returningToSpawn = false;
                return;
            }

            if (agent.remainingDistance < 0.5f)
            {
                returningToSpawn = false;
                SetRandomPatrolAroundSpawn();
            }
            return;
        }

        // ===== 追跡していない =====
        if (!chasing)
        {
            CheckItemOrPlayer();

            if (agent.remainingDistance < 0.5f)
            {
                SetRandomPatrolAroundSpawn();
            }
        }
        // ===== 追跡中 =====
        else
        {
            float dist = Vector3.Distance(transform.position, Player.position);

            if (dist > stopDistance)
                agent.SetDestination(Player.position);
            else
                agent.SetDestination(transform.position);

            if (dist > loseDistance)
            {
                chasing = false;
                returningToSpawn = true;
                agent.SetDestination(spawnPoint);
            }
        }
    }

    // ==============================
    // おとり vs Player 判定
    // ==============================
    void CheckItemOrPlayer()
    {
        if (Item == null)
        {
            GameObject i = GameObject.FindGameObjectWithTag("Item");
            if (i != null) Item = i.transform;
        }

        float playerDist = Player != null
            ? Vector3.Distance(transform.position, Player.position)
            : Mathf.Infinity;

        float itemDist = Item != null
            ? Vector3.Distance(transform.position, Item.position)
            : Mathf.Infinity;

        if (Item != null && itemDist < playerDist)
        {
            agent.SetDestination(Item.position);

            if (itemDist <= stopDistance)
            {
                lured = true;
                lureTimer = lureStopTime;
            }
            return;
        }

        if (PlayerInSight())
        {
            chasing = true;
        }
    }

    // ==============================
    // 視界判定
    // ==============================
    bool PlayerInSight()
    {
        if (Player == null) return false;

        Vector3 eyePos = transform.position + Vector3.up * 1.5f;
        Vector3 dir = (Player.position + Vector3.up - eyePos).normalized;

        if (Vector3.Distance(eyePos, Player.position) > viewDistance)
            return false;

        if (Vector3.Angle(transform.forward, dir) > viewAngle / 2f)
            return false;

        if (Physics.Raycast(eyePos, dir, out RaycastHit hit, viewDistance))
        {
            if (hit.collider.CompareTag("Player"))
                return true;

            if (((1 << hit.collider.gameObject.layer) & obstacleMask) != 0)
                return false;
        }

        return false;
    }

    // ==============================
    // スポナー周辺パトロール
    // ==============================
    void SetRandomPatrolAroundSpawn()
    {
        Vector3 randomPos = spawnPoint + Random.insideUnitSphere * patrolRadius;
        randomPos.y = spawnPoint.y;

        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }


}
