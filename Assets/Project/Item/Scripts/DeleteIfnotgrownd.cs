using UnityEngine;

public class DeleteIfnotgrownd : Item
{
    void Start()
    {
        Invoke("CheckPosition", 5f);
    }

    void CheckPosition()
    {
        if (transform.position.y > 1f)
        {
            Destroy(gameObject);
        }
    }
}
