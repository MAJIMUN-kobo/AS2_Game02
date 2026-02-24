using TMPro;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public TextMeshProUGUI diamondText;
    public int value = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        diamondText.text = "Å~" + value.ToString();
    }
}
