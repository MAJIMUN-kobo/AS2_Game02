using UnityEngine;

public class Clear : MonoBehaviour
{
    [SerializeField, Header("オーディオソース")]
    private AudioSource _ClearAS;

    void Start()
    {
        _ClearAS.Play();
    }
}
