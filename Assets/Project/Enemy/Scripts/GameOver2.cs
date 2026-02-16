using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver2 : MonoBehaviour
{
    // staticF‚Ç‚±‚©‚ç‚Å‚àŒÄ‚×‚é
    public static void GameOverShowPanel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    // š UnityEvent —p
    public void CallGameOver()
    {
        GameOverShowPanel();
    }
}