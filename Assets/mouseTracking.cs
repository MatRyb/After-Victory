using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseTracking : MonoBehaviour
{
    [SerializeField] public GameObject mainObject;
    [SerializeField] public GameObject character1;
    [SerializeField] public GameObject character2;
    
    void Start()
    {
        
    }

    void Update()
    {
    }

    public float getX()
    {
        float tmp =character1.transform.position.x;
        return tmp;
    }

    public float getY()
    {
        return character2.transform.position.y;
    }

    public float getMouseX()
    {
        float tmp=getX() + Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        return tmp;
    }

    public float getMouseY()
    {
        return getY() + Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
    }

}
