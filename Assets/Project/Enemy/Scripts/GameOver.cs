using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    [Header("Player Check")]
    [SerializeField] private string playerTag = "Player";

    public UnityEvent OnEnterPlayer;
    public UnityEvent OnExitPlayer;

    private int enterNum = 0;

    // ===== Player判定（Trigger）=====
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        enterNum++;
        if (enterNum == 1)
        {
            Debug.Log("Player Enter!");
            OnEnterPlayer?.Invoke(); // ★ ここから GameOver2 を呼ぶ
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        enterNum--;
        if (enterNum <= 0)
        {
            OnExitPlayer?.Invoke();
        }
    }

    // ★ Collision での Scene 遷移は削除（役割分離）
}