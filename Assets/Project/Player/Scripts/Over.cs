using UnityEngine;

public class Over : MonoBehaviour
{
    [SerializeField, Header("オーディオソース")]
    private AudioSource _OverAS;

    void Start()
    {
        _OverAS.Play();
    }
}
