using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timer = 0;

    void Update()
    {
        timer += 1 * Time.deltaTime;
        timerText.text = timer.ToString("F2");
    }
}
