using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver2 : MonoBehaviour
{
    // static：どこからでも呼べる
    public static void GameOverShowPanel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    // ★ UnityEvent 用
    public void CallGameOver()
    {
        GameOverShowPanel();
        Debug.Log("CallGameOver 実行！");
        GameOverShowPanel();
    }
}