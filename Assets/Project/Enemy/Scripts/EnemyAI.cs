using UnityEngine;
using ASProject;
using UnityEngine.AI;

public class EnemyAI : BaseCharacter
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

    // ===== 回転制御追加 =====
    [Header("回転制御")]
    public float patrolTurnSpeed = 5f;
    public float chaseTurnSpeed = 10f;

    // ===== 追跡遷移を自然にする =====
    [Header("追跡遷移")]
    public float chaseDelay = 0.3f;
    private float chaseTimer = 0f;

    // ===== 巡回を全方向にする =====
    [Header("巡回タイマー")]
    public float patrolChangeInterval = 8f;
    public float patrolTimer = 0f;

    // ===== 立ち止まって周囲を見る =====
    [Header("警戒動作")]
    public float lookAroundTime = 8f;
    private float lookTimer = 0f;
    private bool lookingAround = false;

    // ===== 円形巡回 =====
    [Header("円形巡回")]
    public int circlePointCount = 1;
    private int circleIndex = 0;
    private Vector3[] circlePoints;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();

        if (Player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) Player = p.transform;
        }

        spawnPoint = transform.position;

        // ===== 円形巡回ポイント生成 =====
        circlePoints = new Vector3[circlePointCount];
        for (int i = 0; i < circlePointCount; i++)
        {
            float angle = i * Mathf.PI * 2f / circlePointCount;
            Vector3 pos = spawnPoint + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * patrolRadius;
            circlePoints[i] = pos;
        }

        agent.SetDestination(circlePoints[0]);
        agent.updateRotation = false;

        SetState(new EnemyStateUpdate(this));
    }

    private void Update()
    {
        /*
        // ===== おとり中 =====
        if ( LuredUpdate() ) return;
        
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

            SmoothRotate();
            return;
        }
        

        // ===== 立ち止まって周囲を見る =====
        if ( LookingAroundUpdate() ) return;
        
        if (lookingAround)
        {
            lookTimer -= Time.deltaTime;
            transform.Rotate(Vector3.up, patrolTurnSpeed * 20f * Time.deltaTime);

            if (lookTimer <= 0f)
            {
                lookingAround = false;
                circleIndex = (circleIndex + 1) % circlePoints.Length;
                agent.SetDestination(circlePoints[circleIndex]);

                lookTimer = 1;
            }
            return;
        }
        

        // ===== スポナーへ帰還 =====
        if (ReturningToSpawnUpdate())return;
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
                agent.SetDestination(circlePoints[circleIndex]);
            }

            SmoothRotate();
            return;
        }

        // ===== 追跡していない =====
        ChasingUpdate();
        /*if (!chasing)
        {
            CheckItemOrPlayer();

            if (agent.remainingDistance < 0.5f)
            {
                lookingAround = true;
                lookTimer = lookAroundTime;
                agent.SetDestination(transform.position);
            }
        }
        // ===== 追跡中 =====
        else
        {
            lured = false;
            lookingAround = false;
            returningToSpawn = false;

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

        SmoothRotate();
        */
    }

    /// <summary>
    /// おとり中の処理
    /// </summary>
    public bool LuredUpdate()
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

            SmoothRotate();
            return true;
        }

        return false;
    }

    public bool LookingAroundUpdate()
    {
        if (lookingAround)
        {
            lookTimer -= Time.deltaTime;
            transform.Rotate(Vector3.up, patrolTurnSpeed * 20f * Time.deltaTime);

            if (lookTimer <= 0f)
            {
                lookingAround = false;
                circleIndex = (circleIndex + 1) % circlePoints.Length;
                agent.SetDestination(circlePoints[circleIndex]);

                lookTimer = 1;
            }
            
            return true;
        }

        return false;
    }

    public bool ReturningToSpawnUpdate()
    {
        if (returningToSpawn)
        {
            if (PlayerInSight())
            {
                chasing = true;
                returningToSpawn = false;
                return true;
            }

            if (agent.remainingDistance < 0.5f)
            {
                returningToSpawn = false;
                agent.SetDestination(circlePoints[circleIndex]);
            }

            SmoothRotate();
            return true;
        }
        return false;

    }

    public void ChasingUpdate()
    {
        if (!chasing)
        {
            CheckItemOrPlayer();

            if (agent.remainingDistance < 0.5f)
            {
                lookingAround = true;
                lookTimer = lookAroundTime;
                agent.SetDestination(transform.position);
            }
        }
        // ===== 追跡中 =====
        else
        {
            lured = false;
            lookingAround = false;
            returningToSpawn = false;

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

        // 見失った時の待ち時間を更新する
        chaseTimer += Time.deltaTime;
        chaseTimer = Mathf.Clamp(chaseTimer, 0, chaseDelay);

        if (PlayerInSight())
        {
            if (chaseTimer >= chaseDelay)
            {
                chasing = true;
                chaseTimer = 0f;
            }
        }
        else
        {
            chaseTimer = 0f;
        }
    }

    // ==============================
    // 視界判定
    // ==============================
    bool PlayerInSight()
    {
        if (Player == null) return false;

        Vector3 dir = (Player.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, dir) <= viewAngle / 2f)
            return true;

        return false;
    }

    // ==============================
    // 回転を滑らかにする
    // ==============================
    public void SmoothRotate()
    {
        if (agent.velocity.sqrMagnitude < 0.01f) return;

        Vector3 dir = agent.velocity.normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir);

        float turnSpeed = chasing ? chaseTurnSpeed : patrolTurnSpeed;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * turnSpeed
        );
    }
}
