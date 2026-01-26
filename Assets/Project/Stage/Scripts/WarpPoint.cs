using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{

    public GameObject AnyObject;

    public Vector3 warpPoint = new Vector3(10f, 0f, 0f);

    

    private void OnCollisionEnter(Collision enemyPrefed)
    {
        enemyPrefed.transform.position = warpPoint;
    }


}