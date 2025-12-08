using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("ˆÚ“®İ’è")]
    private float _moveSpeed;
    private Vector3 _moveVelocity;

    void Start()
    {
        _moveSpeed = 3;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        _moveVelocity = new Vector3(inputX,0,inputZ);

        transform.Translate(_moveVelocity * _moveSpeed * Time.deltaTime);

    }
}
