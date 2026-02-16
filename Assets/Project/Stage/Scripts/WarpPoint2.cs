using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WarpPoint2 : MonoBehaviour
{
    [Header("=== 移動設定")]
    public float moveSpeed = 5.0f;

    [Header("=== パラメータ設定")]
    public float permissibleDamage;

    protected Vector3 _velocity;


    

    private void Update()
    {
        Movement();
        OutOfField();

    }

    protected virtual void OnDamage(float damage)
    {
        if (damage >= permissibleDamage)
        {
            Destroy(gameObject);
        }
    }

    protected void OutOfField()
    {
        if (transform.position.x <= -2000 || transform.position.x >= 2000
            || transform.position.z <= -2000 || transform.position.z >= 2000)
        {
            Destroy(gameObject);
        }

    }

    protected virtual void Movement()
    {

        _velocity = new Vector3(0, 0, 0);

        _velocity.z = moveSpeed * Time.deltaTime;

        //playerのゲームオブジェクトを検索・取得
        GameObject jewelry = GameObject.Find("jewelry");

        if (jewelry == null)
        {
            return;
        }

        if (jewelry.GetComponent<jewelry>().isAlive == false)
        {
            return;
        }

        //指定したゲームオブジェクトに向ける
        transform.LookAt(jewelry.transform);

        //移動させる
        transform.Translate(_velocity);
    }



}