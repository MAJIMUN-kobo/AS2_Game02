using UnityEngine;

public class GameOver : MonoBehaviour
{
    private bool isGameOver = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameOver) return;

        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("Trap"))
        {
            isGameOver = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
    }
}
