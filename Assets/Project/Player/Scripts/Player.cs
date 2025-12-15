using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("ˆÚ“®İ’è")]
    private float _moveSpeed;
    public Vector3 _moveVelocity;

    // ==private•Ï”
    private CameraManager _cameraM;

    void Start()
    {
        _cameraM = GameObject.FindAnyObjectByType<CameraManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Skill();
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // ƒJƒƒ‰‚Æ“¯‚¶Œü‚«‚É‚·‚·‚Ş
        Vector3 cameraForward = Vector3.Scale(_cameraM.transform.forward, new Vector3(1, 0, 1)).normalized;
        _moveVelocity = inputX * _cameraM.transform.right + inputZ * cameraForward;

        // ƒJƒƒ‰‚Æ“¯‚¶•ûŒü‚ğŒü‚­
        if((new Vector2(inputX, inputZ)).magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(_moveVelocity);
        }

        transform.Translate(_moveVelocity * _moveSpeed * Time.deltaTime, Space.World);
    }

    public void Skill()
    {

    }
}
