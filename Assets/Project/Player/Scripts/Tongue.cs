using UnityEngine;
using UnityEngine.UIElements;

public class Tongue : MonoBehaviour
{
    [SerializeField,Header("移動速度")]
    private float _speed = 10.0f;

    // == private変数
    private Player _player;

    void Start()
    {
        _player = GameObject.FindAnyObjectByType<Player>();
    }

    void Update()
    {
        // 移動
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // テレポート関数実行！
        _player.Teleport(transform.position);
        // 自分を消すぜ
        Destroy(gameObject);
    }
}
