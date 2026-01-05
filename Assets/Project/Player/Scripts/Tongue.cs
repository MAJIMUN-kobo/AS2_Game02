using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField,Header("移動速度")]
    private float _speed = 10.0f;

    // == private変数
    private Player Player;

    void Start()
    {
        Player = GameObject.FindAnyObjectByType<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {

    }
    // 喉仏スライダーを作ってた
}
