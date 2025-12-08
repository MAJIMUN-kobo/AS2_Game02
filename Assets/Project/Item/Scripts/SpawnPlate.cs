using UnityEngine;

public class SpawnPlate : MonoBehaviour
{
    [SerializeField] GameObject FishNChips;
    [SerializeField] GameObject Diamond;

    public float range = 15f;

    private void Start()
    {
        SpawnDiamonds();
    }

    void SpawnDiamonds()
    {
        Vector3 basePos = transform.position;

        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);

        Vector3 spawnpos = new Vector3(
            basePos.x + randomX,
            basePos.y,
            basePos.z + randomZ
        );

        Instantiate(Diamond, spawnpos, Quaternion.identity);
    }
}
