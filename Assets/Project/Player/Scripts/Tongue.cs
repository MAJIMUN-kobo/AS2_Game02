using UnityEngine;
using UnityEngine.UIElements;

public class Tongue : MonoBehaviour
{
    [SerializeField,Header("移動速度")]
    private float _speed = 70.0f;

    [SerializeField, Header("rayの長さ")]
    private float _rayDistance;

    // == private変数
    private Player _player;

    void Start()
    {
        _player = GameObject.FindAnyObjectByType<Player>();
    }

    void Update()
    { 
        // rayを飛ばす
        Vector3 rayPosition = transform.position;
        bool rayForward = Physics.Raycast(rayPosition, Vector3.forward, _rayDistance);
        bool rayBack = Physics.Raycast(rayPosition, Vector3.back, _rayDistance);
        bool rayRight = Physics.Raycast(rayPosition, Vector3.right, _rayDistance);
        bool rayLeft = Physics.Raycast(rayPosition, Vector3.left, _rayDistance);
        bool rayDown = Physics.Raycast(rayPosition, Vector3.down, _rayDistance);

        // rayの可視化
        Debug.DrawRay(rayPosition, Vector3.forward * _rayDistance, Color.red);
        Debug.DrawRay(rayPosition, Vector3.back * _rayDistance, Color.red);
        Debug.DrawRay(rayPosition, Vector3.right * _rayDistance, Color.red);
        Debug.DrawRay(rayPosition, Vector3.left * _rayDistance, Color.red);
        Debug.DrawRay(rayPosition, Vector3.down * _rayDistance, Color.red);

        if (rayForward || rayBack || rayRight || rayLeft || rayDown)
        {
            // テレポート関数実行
            _player.Teleport(transform.position);
            Destroy(gameObject);
        }

        // 移動
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
