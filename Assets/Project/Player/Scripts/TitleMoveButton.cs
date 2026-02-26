using UnityEngine;

public class TitleMoveButton : MonoBehaviour
{
    public void OnPushedButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }
}
