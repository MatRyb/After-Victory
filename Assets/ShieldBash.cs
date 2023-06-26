using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : MonoBehaviour
{
    public bool detectionWorking = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        return;
        if (!detectionWorking) return;
        Debug.Log("wiad"); 
        MeleeEnemy target = collision.GetComponent<MeleeEnemy>();
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Shieldbash");
            target.isPushedBack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
        if (!detectionWorking) return;
        Debug.Log("wiad");
        MeleeEnemy target = collision.GetComponent<MeleeEnemy>();
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Shieldbash");
            target.isPushedBack = true;
        }
    }
}

