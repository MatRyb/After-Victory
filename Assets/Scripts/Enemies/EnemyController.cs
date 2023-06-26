using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public float hitPoints;
    public float attackSpeed;
    public float attackDamage;
    public float attackRange;
    public float moveSpeed;
    public bool stun;
    public bool vln = false;

    [SerializeField]
    public GameObject target;

}
