using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    [SerializeField, Header("オーディオソース")]
    private AudioSource _ClearAS;

    [Header("テキストオブジェクト")]
    public TextMeshProUGUI countText;

    void Start()
    {
        _ClearAS.Play();
        countText.text = "x " + PlayerPrefs.GetInt("daidaia");
    }
}
