using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenu;


    public string titleSceneName = "MainGame";

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    public void BackToTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(titleSceneName);
    }
}
