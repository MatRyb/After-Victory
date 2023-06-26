using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DirectionType = HeroSpriteController.DirectionType;


public class SkullDudeSpriteController : MonoBehaviour
{

    public enum FlipFlopType
    {
        Flip = 0,
        Flop = 1,
        Angry = 2,
    };

    public bool IsAngry = false;

    [field: SerializeField]
    private DirectionType direction;
    public DirectionType Direction
    {
        get { return direction; }   // get method
        set { direction = value; SetDirection(direction); }  // set method
    }

    [field: SerializeField]
    private FlipFlopType flipFlop;
    public FlipFlopType FlipFlop
    {
        get { return flipFlop; }
        set { flipFlop = value; SetFlipFlop(flipFlop); }
    }

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
        if (!IsAngry)
            SetFlipFlop(Mathf.CeilToInt(Time.realtimeSinceStartup) % 2 == 0 ? FlipFlopType.Flip : FlipFlopType.Flop);
        else
            SetFlipFlop(FlipFlopType.Angry);
        Billboard();
    }

    void Billboard()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }

    void SetFlipFlop(FlipFlopType type)
    {
        if (m_Material == null) return;
        float heroOffset = (float)type * 0.333333333f;
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
