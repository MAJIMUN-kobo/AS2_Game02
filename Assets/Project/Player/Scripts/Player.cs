using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("移動設定")]
    private float _moveSpeed;
    public Vector3 moveVelocity;

    [Header("スキル設定")]
    public float skillTimer = 0f;
    public float skillTimeMax;

    [SerializeField, Header("舌プレハブ")]
    private GameObject _tongue;

    [SerializeField,Header("喉の位置")]
    private GameObject _throat;

    // ==private変数
    private CameraManager _cameraM;

    void Start()
    {
        _cameraM = GameObject.FindAnyObjectByType<CameraManager>();
    }

    void Update()
    {
        skillTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && skillTimer >= skillTimeMax)
        {
            skillTimer = 0f;
            Skill();
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // カメラと同じ向きにすすむ
        Vector3 cameraForward = Vector3.Scale(_cameraM.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveVelocity = inputX * _cameraM.transform.right + inputZ * cameraForward;

        // カメラと同じ方向を向く
        if((new Vector2(inputX, inputZ)).magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveVelocity);
        }

        transform.Translate(moveVelocity * _moveSpeed * Time.deltaTime, Space.World);
    }

    public void Skill()
    {
        Instantiate(_tongue, _throat.transform.position, transform.rotation);
    }
}
