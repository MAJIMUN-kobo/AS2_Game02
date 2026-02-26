using UnityEngine;
using UnityEngine.InputSystem;
using ASProject;

public class Player : BaseCharacter
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
    public int DiamondPurpose = 5;

    [SerializeField, Header("rayの位置")]
    private GameObject _rayPos;

    [SerializeField, Header("rayの長さ")]
    private float _rayDistance = 2.5f ;

    [SerializeField, Header("アイテムを使用したときの位置")]
    private GameObject _itemUsePos;

    [Header("インベントリ")]
    public Item Inventory;

    [SerializeField, Header("オーディオソース")]
    private AudioSource _AS;

    [SerializeField, Header("BGMオーディオソース")]
    private AudioSource _GameAS;

    [Header("クリップ")]
    public AudioClip SkillSE;
    public AudioClip SkillRecoverySE;
    public AudioClip ItemUseSE;
    public AudioClip DiamondSE;
    public AudioClip ItemSE;

    public Animator anim;

    // ==private変数
    private bool _animeTimer = false;
    private float _skillAnimeTimer;
    private CameraManager _cameraM;
    private bool _isSRParticle = true;
    private ParticleSystem _sRParticleClone;

    protected override void Start()
    {
        _cameraM = GameObject.FindAnyObjectByType<CameraManager>();
        anim = this.GetComponentInChildren<Animator>();
        _GameAS.Play();
    }

    protected override void Update()
    {
        base.Update();
    }

    // ======パーティクル削除=====
    public void DestroyParticle()
    {
        // Destroyのついたタグを探す
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Destroy");
        // nodoに一つずつ入れて3秒後に消す
        foreach (GameObject nodo in objects)
        {
            Destroy(nodo, 3);
        }
    }

    // =====スキルタイマー=====
    public void SkillTimerUpdate()
    {
        SkillTimer += Time.deltaTime;

        if (SkillTimer >= SkillTimeMax)
        {
            if (_isSRParticle == true)
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
    }

    // =====プレイヤー移動=====
    public void PlayerMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // カメラの向き前後左右
        Vector3 cameraForward = Vector3.Scale(_cameraM.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(_cameraM.transform.right, new Vector3(1, 0, 1)).normalized;

        // rayを飛ばす
        Vector3 rayPosition = _rayPos.transform.position;
        bool rayForward = Physics.Raycast(rayPosition, cameraForward, _rayDistance);
        bool rayBack = Physics.Raycast(rayPosition, -cameraForward, _rayDistance);
        bool rayRight = Physics.Raycast(rayPosition, cameraRight, _rayDistance);
        bool rayLeft = Physics.Raycast(rayPosition, -cameraRight, _rayDistance);

        // rayの可視化
        Debug.DrawRay(rayPosition, cameraForward * _rayDistance, Color.red);
        Debug.DrawRay(rayPosition, -cameraForward * _rayDistance, Color.red);
        Debug.DrawRay(rayPosition, cameraRight * _rayDistance, Color.red);
        Debug.DrawRay(rayPosition, -cameraRight * _rayDistance, Color.red);

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
            anim.SetBool("Run", true);
        }
        else anim.SetBool("Run", false);

        // 移動
        transform.Translate(moveVelocity * _moveSpeed * Time.deltaTime, Space.World);

        // 下にRayを飛ばす
        RaycastHit hit;
        if (Physics.Raycast(rayPosition + Vector3.up, Vector3.down, out hit, 3.0f, LayerMask.GetMask("Stage")))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    // =====アイテム使用=====
    public void ItemUse()
    {
        if (Input.GetMouseButtonDown(1) && Inventory != null)
        {
            _AS.PlayOneShot(ItemUseSE, 0.3f);
            Inventory.gameObject.transform.position = _itemUsePos.transform.position;
            Inventory.gameObject.SetActive(true);
            Inventory.isUse = true;
            Destroy(Inventory.gameObject,10.0f);
            Inventory = null;
        }
    }

    // =====喉仏複成=====
    public void Skill()
    {
        anim.SetBool("Skill", true);
        _skillAnimeTimer = 0;
        _animeTimer = true;
        _AS.PlayOneShot(SkillSE,0.25f);
        // 喉仏を複成
        Instantiate(_tongue, _throat.transform.position, transform.rotation);
        // パーティクル発動できますよ～
        _isSRParticle = true;
        // スキルタイマーリセット
        SkillTimer = 0.0f;
    }

    // =====アニメタイマー=====
    public void SkillAnimeTimerUpdate()
    {
        if (_animeTimer == true)
        {
            _skillAnimeTimer += Time.deltaTime;
            if(_skillAnimeTimer >= 0.2f)
            {
                anim.SetBool("Skill", false);
                _animeTimer = false;
            }
        }  
    }

    // ======テレポート=====
    public void Teleport(Vector3 TeleportDestination)
    {
        // 喉仏の場所にテレポートする
        transform.position = new Vector3(TeleportDestination.x,transform.position.y,TeleportDestination.z);
    }

    // =====パーティクル複成=====
    public void sRParticle()
    {
        _AS.PlayOneShot(SkillRecoverySE, 0.3f);
        // sRParticleClonにヒエラルキー指定したパーティクルを入れる
        _sRParticleClone = Instantiate(_sRParticleS, _sRParticleParent);
        // パーティクル駄目！絶対！
        _isSRParticle = false;
    }

    // =====衝突=====
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Goal" && GameManager.Instance.diamondCollect == DiamondPurpose)
        {
            _AS.Stop();
            // unityengineの方からSceneManagement使えよ！
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClearScene");
            GameManager.Instance.CursorSetActive(true);
        }

        if (collision.gameObject.tag == "Item" && Inventory == null)
        {
            _AS.PlayOneShot(ItemSE, 0.4f);
            Item item = collision.gameObject.GetComponent<Item>();
            if (item.isUse == false)
            {
                Inventory = item;
                Inventory.gameObject.SetActive(false);
            }
        }
        
        if (collision.gameObject.tag == "Diamond")
        {
            _AS.PlayOneShot(DiamondSE, 0.5f);
            GameObject diamond = collision.gameObject;
            GameManager.Instance.AddDiamond(1);
            diamond.gameObject.SetActive(false);
        }
    }
}
