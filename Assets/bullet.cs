using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        Destroy(gameObject, 10.0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController e = collision.gameObject.GetComponent<EnemyController>();
        if (e.vln)
        {
            e.hitPoints -= 5;
        }
        else
        {
            e.hitPoints -= 1;
            e.vln = true;
        }
        Destroy(gameObject);
    }
}
