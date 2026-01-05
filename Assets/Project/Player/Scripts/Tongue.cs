using UnityEngine;
using UnityEngine.UIElements;

public class Tongue : MonoBehaviour
{
    [SerializeField,Header("ˆÚ“®‘¬“x")]
    private float _speed = 10.0f;

    // == private•Ï”
    private Player _player;

    void Start()
    {
        _player = GameObject.FindAnyObjectByType<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        _player.Teleport(transform.position);
        Destroy(gameObject);
    }
}
