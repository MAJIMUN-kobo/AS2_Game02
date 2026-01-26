using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    [SerializeField, Header("移動設定")]
    private float _moveSpeed;
    public Vector3 moveVelocity;

    [Header("スキル設定")]
    public float SkillTimer = 0f;
    public float SkillTimeMax;

    [SerializeField, Header("舌プレハブ")]
    private GameObject _tongue;

    [SerializeField,Header("喉の位置")]
    private GameObject _throat;

    [SerializeField,Header("パーティクルプレハブ")]
    private ParticleSystem _sRParticleS;

    [SerializeField, Header("パーティクルの親")]
    private Transform _sRParticleParent;

    [Header("ダイヤの数")]
    public int DamondHave = 0;
    public int DamondPurpose;

    [SerializeField, Header("rayの長さ")]
    private float rayDistance;

    // ==private変数
    private CameraManager _cameraM;
    private bool _isSRParticle = true;
    private ParticleSystem _sRParticleClone;

    void Start()
    {
        _cameraM = GameObject.FindAnyObjectByType<CameraManager>();
    }

    void Update()
    {
        #region パーティクル削除
        // Destroyのついたタグを探す
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Destroy");
        // nodoに一つずつ入れて3秒後に消す
        foreach (GameObject nodo in objects)
        {
            Destroy(nodo,3);
        }
        #endregion

        #region スキルタイマー


        SkillTimer += Time.deltaTime;

        if(SkillTimer >= SkillTimeMax)
        {
            if(_isSRParticle == true)
            {
                // 溜まったらパーティクル発動
                sRParticle();
            }

            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
            {
                // スキル発動
                Skill();
            }
        }

        #endregion

        #region プレイヤー移動

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // カメラの向き前後左右
        Vector3 cameraForward = Vector3.Scale(_cameraM.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(_cameraM.transform.right, new Vector3(1, 0, 1)).normalized;

        // rayを飛ばす
        Vector3 rayPosition = transform.position;
        bool rayForward = Physics.Raycast(rayPosition, cameraForward, rayDistance);
        bool rayBack = Physics.Raycast(rayPosition, -cameraForward, rayDistance);
        bool rayRight = Physics.Raycast(rayPosition, cameraRight, rayDistance);
        bool rayLeft = Physics.Raycast(rayPosition, -cameraRight, rayDistance);

        // rayの可視化
        Debug.DrawRay(rayPosition, cameraForward * rayDistance, Color.red);
        Debug.DrawRay(rayPosition, -cameraForward * rayDistance, Color.red);
        Debug.DrawRay(rayPosition, cameraRight * rayDistance, Color.red);
        Debug.DrawRay(rayPosition, -cameraRight * rayDistance, Color.red);

        if (rayForward && inputZ > 0) inputZ = 0;
        if (rayBack && inputZ < 0) inputZ = 0;
        if (rayRight && inputX > 0) inputX = 0;
        if (rayLeft && inputX < 0) inputX = 0;

        // カメラの向きに移動
        moveVelocity = inputX * _cameraM.transform.right + inputZ * cameraForward;

        // カメラと同じ方向を向く
        if ((new Vector2(inputX, inputZ)).magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveVelocity);
        }

        transform.Translate(moveVelocity * _moveSpeed * Time.deltaTime, Space.World);

        RaycastHit hit;
        if(Physics.Raycast(rayPosition,Vector3.down, out hit, 10.0f))
        {
            Vector3 GroundPos = hit.point;                          // ↓次はここを変える
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        #endregion
    }

    // 喉仏複成
    public void Skill()
    {
        // 喉仏を複成
        Instantiate(_tongue, _throat.transform.position, transform.rotation);
        // パーティクル発動できますよ～
        _isSRParticle = true;
        // スキルタイマーリセット
        SkillTimer = 0.0f;
    }

    // テレポート
    public void Teleport(Vector3 TeleportDestination)
    {
        // 喉仏の場所にテレポートする
        transform.position = new Vector3(TeleportDestination.x,transform.position.y,TeleportDestination.z);
    }

    // パーティクル複成
    public void sRParticle()
    {
        // sRParticleClonにヒエラルキー指定したパーティクルを入れる
        _sRParticleClone = Instantiate(_sRParticleS, _sRParticleParent);
        // パーティクル駄目！絶対！
        _isSRParticle = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Goal" && DamondHave == DamondPurpose)
        {
            // unityengineの方からSceneManagement使えよ！
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClearScene");
        }
    }
}
