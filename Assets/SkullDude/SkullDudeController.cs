using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDudeController : MonoBehaviour
{
    private Vector2 lastPos;
    private Vector2 motionDirection;
    private SkullDudeSpriteController spriteController;

    public bool IsAngry = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteController = GetComponentInChildren<SkullDudeSpriteController>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteController.IsAngry = IsAngry;
        motionDirection = (new Vector2(transform.position.x, transform.position.y) - lastPos);
        if (motionDirection.magnitude < 0.0001f)
        {
            return;
        }
        lastPos = transform.position;

        spriteController.Direction = ResolveDirection(motionDirection);
    }

    HeroSpriteController.DirectionType ResolveDirection(Vector3 motionDirection)
    {
        Vector2 dir = motionDirection;

        Vector2 left = new Vector2(-1, 0);
        Vector2 right = new Vector2(1, 0);
        Vector2 up = new Vector2(0, 1);
        Vector2 down = new Vector2(0, -1);

        float leftWeight = Vector2.Dot(dir, left);
        float rightWeight = Vector2.Dot(dir, right);
        float upWeight = Vector2.Dot(dir, up);
        float downWeight = Vector2.Dot(dir, down);

        float maxWeight = Mathf.Max(leftWeight, rightWeight, upWeight, downWeight);

        this.transform.position = new Vector3(this.transform.transform.position.x, this.transform.transform.position.y, this.transform.transform.position.y);

        if (maxWeight == leftWeight) return HeroSpriteController.DirectionType.Left;
        if (maxWeight == rightWeight) return HeroSpriteController.DirectionType.Right;
        if (maxWeight == upWeight) return HeroSpriteController.DirectionType.Up;
        if (maxWeight == downWeight) return HeroSpriteController.DirectionType.Down;



        Debug.LogError("Direction could not be resolved.");
        throw new System.Exception("Direction not found.");
    }
}
