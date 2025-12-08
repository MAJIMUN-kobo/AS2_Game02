using UnityEngine;

public class TestPlayerManager : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5;
    public Vector3 moveVelocity;

    private TestCameraManager _camera;
    void Start()
    {
        _camera = FindAnyObjectByType<TestCameraManager>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 cameraFoward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
        moveVelocity = inputX * _camera.transform.right + inputY * cameraFoward;

        transform.Translate(moveVelocity * moveSpeed * Time.deltaTime, Space.World);
    }
}
