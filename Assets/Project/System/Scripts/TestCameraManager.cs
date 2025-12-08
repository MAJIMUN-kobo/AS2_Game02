using UnityEngine;

public class TestCameraManager : MonoBehaviour
{
    public Transform parent;
    public Transform child;
    public Transform mainCamera;
    public Transform lookTarget;

    public float distance = 2;
    public Vector2 angles;
    public float chiltMin = -90;
    public float chiltMax = 90;

    [Range(1, 50)]
    public float mouceSensitivity = 5f;

    void Start()
    {

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        angles.x += mouseY * mouceSensitivity;
        angles.y += mouseX * mouceSensitivity;
        angles.x = Mathf.Clamp(angles.x, 0, 90);
        parent.position = lookTarget.position;
        child.localPosition = new Vector3(0, 0, -distance);
        parent.eulerAngles = new Vector3(angles.x, angles.y, 0);
    }
}
