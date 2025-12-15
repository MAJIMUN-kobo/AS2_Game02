using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // シーンの名前
    public string titleSceneName = "TitleScene";
    public string gameSceneName = "GameScene";
    public string resultSceneName = "ResultScene";

    // タイトル画面読み込み
    public void LoadTitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(titleSceneName);
    }

    // ゲーム画面読み込み
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }

    // リザルト画面読み込み
    public void LoadResultScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(resultSceneName);
    }

    // ゲーム終了
    public void QuitGame()
    {
        Application.Quit();
    }
}
