using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    [SerializeField] GameObject Diamond;

    public float range = 0f;

    private void Start()
    {
        SpawnDiamonds();
    }

    void SpawnDiamonds()
    {
        Instantiate(Diamond, GetRandomPos(), Quaternion.identity);
    }

    Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;

        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);

        return new Vector3(
            basePos.x,
            basePos.y,
            basePos.z
        );
    }
}
