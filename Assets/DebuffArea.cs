using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController target = collision.GetComponent<EnemyController>();
            if (target.vln)
            {
                target.moveSpeed *= 0.35f;
            }
            else
            {
                target.moveSpeed *= 0.55f;
            }
        }
    }
}
