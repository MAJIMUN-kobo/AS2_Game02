using UnityEngine;

public class SpawnPlate : MonoBehaviour
{
    [SerializeField] GameObject FishNChips;
    [SerializeField] GameObject Diamond;

    public float range = 15f;

    private void Start()
    {
        SpawnDiamonds();
        int lop = 0;
        int rand = Random.Range(5, 10);
        while (lop < rand)
        {
            SpawnPlates();
            lop++;
        }
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

    void SpawnPlates()
    {
        Vector3 basePos = transform.position;

        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);

        Vector3 spawnpos = new Vector3(
            basePos.x + randomX,
            basePos.y,
            basePos.z + randomZ
        );

        Instantiate(FishNChips, spawnpos, Quaternion.identity);
    }
}
