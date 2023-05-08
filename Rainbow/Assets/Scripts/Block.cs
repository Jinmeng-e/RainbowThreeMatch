using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Image icon;
    bool isVerticalMove = false;
    float addedMoveValue = 0f;
    float lastMoveValue = 0f;
    public Color Color => color;

    Color color = Color.white;
    int colorIndex;
    Vector2 positionIndex;

    //Position
    public void SetPosition(Vector2 pos)
    {
        positionIndex = pos;
    }
    //Color
    public void InitColor(int index)
    {
        this.colorIndex = index;
        this.color = ColorHelper.instance.GetColor(index);
    }
    public void ChangeColor()
    {
        ++colorIndex;
        ColorHelper.instance.GetColor(colorIndex);
    }

    void MoveVertical(float v)
    {
        addedMoveValue += v;
        lastMoveValue = v;
        if (addedMoveValue > PositionHelper.instance.vHarf)
        {
            OnMove();
        }
    }
    void MoveHorizontal(float h)
    {
        addedMoveValue += h;
        lastMoveValue = h;
        if(addedMoveValue > PositionHelper.instance.hHarf)
        {
            OnMove();
        }
    }
    void OnMove()
    {
        addedMoveValue = 0;
        lastMoveValue = 0;
    }


    //Move
    public void OnBeginDrag(PointerEventData eventData)
    {
        isVerticalMove = eventData.delta.x < eventData.delta.y;
        if (isVerticalMove)
        {
            MoveVertical(eventData.delta.y);
        }
        else
        {
            MoveHorizontal(eventData.delta.x);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isVerticalMove)
        {
            MoveVertical(eventData.delta.y);
        }
        else
        {
            MoveHorizontal(eventData.delta.x);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnMove();
    }
}
