using UnityEngine;

public class SpawnPlate : MonoBehaviour
{
    [SerializeField] GameObject FishNChips;
    [SerializeField] GameObject MovableObj;
    [SerializeField] GameObject PoisonFishNChips;

    public float range = 15f;

    private void Start()
    {

        int totalCount = Random.Range(5, 20);

        SpawnItem(FishNChips);
        SpawnItem(MovableObj);
        SpawnItem(PoisonFishNChips);

        int spawned = 3;

        while (spawned < totalCount)
        {
            SpawnRandomPlate();
            spawned++;
        }

        Destroy(gameObject);
    }

    void SpawnRandomPlate()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                SpawnItem(FishNChips);
                break;
            case 1:
                SpawnItem(MovableObj);
                break;
            case 2:
                SpawnItem(PoisonFishNChips);
                break;
        }
    }

    void SpawnItem(GameObject obj)
    {
        Instantiate(obj, GetRandomPos(), Quaternion.identity);
    }

    Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;

        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);

        return new Vector3(
            basePos.x + randomX,
            basePos.y,
            basePos.z + randomZ
        );
    }
}
