using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class HeroSpriteController : MonoBehaviour
{
    public enum DirectionType
    {
        Up = 0,
        Down = 1,
        Right = 2,
        Left = 3
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
        SetDirection(direction);
        SetHero(hero);
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection(direction);
        SetHero(hero);
    }

    void SetHero(HeroType type)
    {
        float heroOffset = (float)type * 0.25f;
        //m_Material.mainTextureOffset.y = directionOffset;

        Vector2 offset = m_Material.mainTextureOffset;
        Vector2 newOffset = new Vector2(heroOffset, offset.y);
        m_Material.mainTextureOffset = newOffset;
    }

    void SetDirection(DirectionType type)
    {
        float directionOffset = (float)type * -0.25f;
        //m_Material.mainTextureOffset.y = directionOffset;

        Vector2 offset = m_Material.mainTextureOffset;
        Vector2 newOffset = new Vector2(offset.x, directionOffset);
        m_Material.mainTextureOffset = newOffset;
    }
}
