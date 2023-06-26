using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterScript : MonoBehaviour
{
    public enum champions
    {
        WARRIOR,
        ARCHER,
        GUARDIAN,
        MAGE
    }

    public float health = 16;
    public champions champion;

    // Update is called once per frame
    void Update()
    {
        
    }
}
