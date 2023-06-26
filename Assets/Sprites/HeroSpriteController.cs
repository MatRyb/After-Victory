using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpriteController : MonoBehaviour
{
    public enum DirectionType
    {
        Left = 0,
        Right = 1,
        Down = 2,
        Up = 3,
    };

    public enum HeroType
    {
        Warrior = 0,
        Archer = 1,
        Mage = 2,
        Guardian = 3
    };


    [field: SerializeField]
    private DirectionType direction;
    public DirectionType Direction
    {
        get { return direction; }   // get method
        set { direction = value; SetDirection(direction); }  // set method
    }

    [field: SerializeField]
    private HeroType hero;
    public HeroType Hero
    {
        get { return hero; }
        set { hero = value; SetHero(hero); }
    }

    [SerializeField]
    private Material m_Material;

    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection(direction);
        SetHero(hero);
        Billboard();
    }

    void Billboard()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }

    void SetHero(HeroType type)
    {
        if (m_Material == null) return;
        float heroOffset = (float)type * 0.25f;
        //m_Material.mainTextureOffset.y = directionOffset;

        Vector2 offset = m_Material.mainTextureOffset;
        Vector2 newOffset = new Vector2(heroOffset, offset.y);
        m_Material.mainTextureOffset = newOffset;
    }

    void SetDirection(DirectionType type)
    {
        if (m_Material == null) return;
        float directionOffset = (float)type * 0.25f;
        //m_Material.mainTextureOffset.y = directionOffset;
        
        Vector2 offset = m_Material.mainTextureOffset;
        Vector2 newOffset = new Vector2(offset.x, directionOffset);
        m_Material.mainTextureOffset = newOffset;
    }
}
