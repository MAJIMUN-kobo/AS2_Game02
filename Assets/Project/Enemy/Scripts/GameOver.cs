using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    [Header("Player Check")]
    [SerializeField] private string playerTag = "Player";

    public UnityEvent OnEnterPlayer;
    public UnityEvent OnExitPlayer;

    private int enterNum = 0;

    // ===== Player”»’èiTriggerj=====
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        enterNum++;
        if (enterNum == 1)
        {
            Debug.Log("Player Enter!");
            OnEnterPlayer?.Invoke(); // š ‚±‚±‚©‚ç GameOver2 ‚ğŒÄ‚Ô
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

    // š Collision ‚Å‚Ì Scene ‘JˆÚ‚Ííœ
}