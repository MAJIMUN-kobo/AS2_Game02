using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MinimapIconManager : MonoBehaviour
{
    [SerializeField] private Camera minimapCamera;      // ミニマップ用カメラ
    [SerializeField] private Transform iconTarget;      // アイコンに対応するオブジェクト（建造物等）
    [SerializeField] private float rangeRadius = 1.0f;  // 表示範囲

    // 必要なコンポーネント
    private SpriteRenderer spriteRenderer;

    private float defaultPosY;         // アイコンのデフォルトY座標
    const float normalAlpha = 1.0f;    // 範囲内のアルファ値
    const float outRangeAlpha = 0.5f;  // 範囲外のアルファ値

    // 初期化処理
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();  // SpriteRendererコンポーネントを取得
        defaultPosY = transform.position.y;  // デフォルトのY座標を保存
    }

    // 更新処理
    private void Update()
    {
        DispIcon(); // アイコン表示を更新
    }

    /// <summary>
    /// アイコン表示を更新する
    /// </summary>
    private void DispIcon()
    {
        // アイコンを表示する座標
        var iconPos = new Vector3(iconTarget.position.x, defaultPosY, iconTarget.position.z);

        // ミニマップ範囲内の場合はそのまま表示する
        if (CheckInsideMap())
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, normalAlpha);
            transform.position = iconPos;
            return;
        }

        // マップ範囲外の場合、ミニマップ端までのベクトルを求めて半透明で表示する
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, outRangeAlpha);
        var centerPos = new Vector3(minimapCamera.transform.position.x, defaultPosY, minimapCamera.transform.position.z);
        var offset = iconPos - centerPos;
        transform.position = centerPos + Vector3.ClampMagnitude(offset, rangeRadius);
    }

    /// <summary>
    /// オブジェクトがミニマップ範囲内にあるか確認する
    /// </summary>
    /// <returns>ミニマップ範囲内の場合、trueを返す</returns>
    private bool CheckInsideMap()
    {
        var cameraPos = minimapCamera.transform.position;
        var targetPos = iconTarget.position;

        // 直線距離で判定するため、yは0扱い
        cameraPos.y = targetPos.y = 0;

        //Debug.Log(Vector3.Distance(cameraPos, targetPos)); （デバッグ用）
        return Vector3.Distance(cameraPos, targetPos) <= rangeRadius;  // 範囲内かをreturn
    }
}
