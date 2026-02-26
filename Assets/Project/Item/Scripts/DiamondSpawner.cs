using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    [SerializeField] GameObject Diamond;

    private void Start()
    {
        SpawnDiamonds();

        Destroy(gameObject);
    }

    void SpawnDiamonds()
    {
        Instantiate(Diamond, GetRandomPos(), Quaternion.identity);
    }

    Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;

        return new Vector3(
            basePos.x,
            basePos.y,
            basePos.z
        );
    }
}
