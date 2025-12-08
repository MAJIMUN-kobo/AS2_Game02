using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("情報収集")]
    public Transform Parent;
    public Transform Child;
    public Transform Camera;
    public Transform LookTarget;

    [Header("距離設定"), Range(-5, 5)]
    public float Distance = -4;

    [Header("角度設定"),Range(10, 60)]
    public float Chilt = 20;

    [Header("感度設定"), Range(1, 30)]
    public float MouseIntensity = 6;

    [Header("角度等々")]
    public Vector2 Angles;
    public Vector3 positionOffset;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        Angles.y += mouseX * MouseIntensity;

        Angles.x = Chilt;       // 角度制限

        Parent.position = LookTarget.position + positionOffset;     // 親をターゲットに移動

        Child.localPosition = new Vector3(0, 0, -Distance);         // 距離をあける

        Parent.eulerAngles = new Vector3(Angles.x, Angles.y, 0);    // 親の角度の更新
    }
}
