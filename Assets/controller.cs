using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject character4;

    public GameObject area1;
    public GameObject area2;
    public GameObject area3;
    public GameObject area4;

    characterScript character1Script;
    characterScript character2Script;
    archerController character3Script;
    characterScript character4Script;

    squareOnClick squareOnClickScript1;
    squareOnClick squareOnClickScript2;
    squareOnClick squareOnClickScript3;
    squareOnClick squareOnClickScript4;

    [SerializeField] private float movementSpeed = 0.05f;
    [SerializeField] private Rigidbody2D Player;

    float HORIZONTAL_MOVEMENT;
    float VERTICAL_MOVEMENT;

    private bool isE_pressedDown;
    private bool isQ_pressedDown;
    public bool character3CanAttack = true;
    public GameObject slash;

    void Start()
    {
        character1Script = character1.GetComponent<characterScript>();
        character2Script = character2.GetComponent<characterScript>();
        character3Script = character3.GetComponent<archerController>();
        character4Script = character4.GetComponent<characterScript>();

        squareOnClickScript1 = area1.GetComponent<squareOnClick>();
        squareOnClickScript2 = area2.GetComponent<squareOnClick>();
        squareOnClickScript3 = area3.GetComponent<squareOnClick>();
        squareOnClickScript4 = area4.GetComponent<squareOnClick>();
    }

    void Update()
    {
        isRotateKeysDown();
        atack();
    }

    private void FixedUpdate()
    {
        rotate();
        
    }

    public void rotate()
    {
        if (isQ_pressedDown) 
        {
            transform.Rotate(0f, 0f, 2f);
        }
        else if (isE_pressedDown)
        {
            transform.Rotate(0f, 0f, -2f);
        }
    }
    
    public void isRotateKeysDown()
    {
        eDown();
        qDown();
    }
    public void eDown()
    {
        if(Input.GetKey(KeyCode.E)){
            isE_pressedDown = true;
        }
        else
        {
            isE_pressedDown = false;
        }
    }
    public void qDown()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            isQ_pressedDown = true;
        }
        else
        {
            isQ_pressedDown = false;
        }
    }

    public void atack()
    {
        atack1();
        atack2();
        atack3();
        atack4();
    }

    public void atack1()
    {
        if(squareOnClickScript1.isAreaClicked())
        {
            character1.GetComponent<warriorController>().Attack();
            Debug.Log("AREA 1 CLICKED");
        }
    }

    public void atack2()
    {
        if (squareOnClickScript2.isAreaClicked())
        {
            character2.GetComponent<guardianController>().Attack();
            Debug.Log("AREA 2 CLICKED");
        }
    }

    public void atack3()
    {
        StartCoroutine(archerCooldown());
    }
    
    public IEnumerator archerCooldown()
    {
        if (squareOnClickScript3.isAreaClicked())
        {
            if (character3CanAttack)
            {
                character3CanAttack = false;
                character3Script.attack();
                yield return new WaitForSeconds(0.5f);
                character3CanAttack = true;
            }
            
            Debug.Log("AREA 3 CLICKED");
        }       
    }

    public void atack4()
    {
        if (squareOnClickScript4.isAreaClicked())
        {
            character4.GetComponent<mageController>().Attack();
            Debug.Log("AREA 4 CLICKED");
        }
    }
}
