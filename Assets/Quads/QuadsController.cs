using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadsController : MonoBehaviour
{
    public GameObject[] collisionCheckers;
    public GameObject[] drawingPlanes;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < collisionCheckers.Length; i++)
        {
            squareOnClick collider = collisionCheckers[i].GetComponent<squareOnClick>();
            QuadDrawingScript drawer = drawingPlanes[i].GetComponentInChildren<QuadDrawingScript>();

            drawer.isEnabled = collider.IsHover;
        }
    }
}
