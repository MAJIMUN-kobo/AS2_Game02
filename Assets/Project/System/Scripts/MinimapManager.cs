using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private Transform player;  // プレイヤーをアタッチ
    [SerializeField] private Transform mainCamera; // メインカメラをアタッチ

    // 更新処理
    void Update()
    {
        // ミニマップの位置をプレイヤーに合わせる
        var pos = player.transform.position;
        pos.y = transform.position.y;
        transform.position = pos;

        // ミニマップの回転をメインカメラに合わせる
        transform.rotation = Quaternion.Euler(90f, mainCamera.eulerAngles.y, 0f);
    }
}
