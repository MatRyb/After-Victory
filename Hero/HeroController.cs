using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class HeroController : MonoBehaviour
{
    private HeroSpriteController m_SpriteController;
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteController = GetComponentInChildren<HeroSpriteController>();
        if (m_SpriteController == null)
        {
            Debug.LogError("Child SpriteController not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform parentTransform = this.transform.parent;
        if (parentTransform)
        {
            HeroSpriteController.DirectionType dirType = ResolveDirection(parentTransform, this.transform);
            m_SpriteController.Direction = dirType;
        } else
        {
            Debug.LogError("ParentTransform not found");
        }
    }

    static HeroSpriteController.DirectionType ResolveDirection(Transform parent, Transform target)
    {
        Vector2 fromPos = parent.transform.position;
        Vector2 toPos = target.position;

        Vector2 dir = (toPos - fromPos).normalized;

        Vector2 left = new Vector2(-1, 0);
        Vector2 right = new Vector2(1, 0);
        Vector2 up = new Vector2(0, 1);
        Vector2 down = new Vector2(0, -1);

        float leftWeight = Vector2.Dot(dir, left);
        float rightWeight = Vector2.Dot(dir, right);
        float upWeight = Vector2.Dot(dir, up);
        float downWeight = Vector2.Dot(dir, down);

        float maxWeight = Mathf.Max(leftWeight, rightWeight, upWeight, downWeight);

        if (maxWeight == leftWeight) return HeroSpriteController.DirectionType.Left;
        if (maxWeight == rightWeight) return HeroSpriteController.DirectionType.Right;
        if (maxWeight == upWeight) return HeroSpriteController.DirectionType.Up;
        if (maxWeight == downWeight) return HeroSpriteController.DirectionType.Down;

        Debug.LogError("Direction could not be resolved.");
        throw new System.Exception("Direction not found.");
    }


}
