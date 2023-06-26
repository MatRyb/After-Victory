using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class squareOnClick : MonoBehaviour
{
    private bool _isHover = false;
    public bool IsHover { get { return _isHover;  } }

    private void Update()
    {
        _isHover = isAreaHover();
    }
    public bool isAreaClicked()
    {
        return Input.GetMouseButtonDown(0) && isAreaHover();
    }

    private bool isAreaHover()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject.name == gameObject.name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

}
