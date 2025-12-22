using UnityEngine;

public class Item : MonoBehaviour
{

    public bool isUse = false;
    public float effectRange = 10f;
    public EnemyAI[] target;
    public LayerMask layerMask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRange, ~(1 << layerMask));
        target = new EnemyAI[hitColliders.Length];
        if (isUse == false)
        {
            return;
        }
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log($"hit: { hitColliders[i].name }");

            if (hitColliders[i].transform.tag == "Enemy")
            {
                target[i] = hitColliders[i].GetComponent<EnemyAI>();
                target[i].Player = transform;
                target[i].chasing = true;
                Debug.Log(hitColliders[i].name);
            }
        }
    }
}
