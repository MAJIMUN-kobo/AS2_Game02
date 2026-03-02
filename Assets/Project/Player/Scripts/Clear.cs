using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    [SerializeField, Header("オーディオソース")]
    private AudioSource _ClearAS;

    [Header("テキストオブジェクト")]
    public TextMeshProUGUI countText;

    // 間に合わなかった
    //[Header("タイムテキストオブジェクト")]
    //public TextMeshProUGUI timeText;

    void Start()
    {
        _ClearAS.Play();
        countText.text = "x " + PlayerPrefs.GetInt("daidaia");
        //timeText.text = PlayerPrefs.GetInt("time").ToString();

    }
}
