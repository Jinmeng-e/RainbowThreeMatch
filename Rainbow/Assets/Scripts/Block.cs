using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI txt;// 임시 
    bool isVerticalMove = false;
    float addedMoveValue = 0f;
    float lastMoveValue = 0f;
    public Color Color => color;
    public int colorIndex => data.colorIndex;

    Color color = Color.white;
    BlockData data;
    bool isLocked = false;
    void DebugText(int index)
    {
        if (txt == null) { txt = GetComponentInChildren<TextMeshProUGUI>(); }
        if (txt != null)
        {
            txt.text = index + "";
            txt.color = Color.black;
        }
    }

    void MoveVertical(float v)
    {
        addedMoveValue += v;
        lastMoveValue = v;
        var pos = icon.transform.position;
        pos.x = transform.position.x + addedMoveValue;
        if (addedMoveValue > PositionHelper.instance.vHarf && data.y > 0)
        {
            // swap
            var pre = data;
            data.y--;
            Game.instance.Swap(pre, data);
        }
        if(addedMoveValue < PositionHelper.instance.vHarf && data.y < 6)
        {
            // swap
            var pre = data;
            data.y++;
            Game.instance.Swap(pre, data);
        }
        OnMove();
    }
    void MoveHorizontal(float h)
    {
        addedMoveValue += h;
        lastMoveValue = h;
        var pos = icon.transform.position;
        pos.x = transform.position.y + addedMoveValue;
        if (addedMoveValue > PositionHelper.instance.hHarf && data.x > 0)
        {
            var pre = data;
            data.x++;
            Game.instance.Swap(pre, data);
        }
        if(addedMoveValue < PositionHelper.instance.hHarf && data.x < 6)
        {
            var pre = data;
            data.x--;
            Game.instance.Swap(pre, data);
        }
        OnMove();
    }
    void OnMove()
    {
        addedMoveValue = 0;
        lastMoveValue = 0;
        // back to position;
        StartCoroutine(MoveToIndex());

    }
    IEnumerator MoveToIndex()
    {
        yield return null;
        var dest = PositionHelper.instance.GetPos((int)data.x, (int)data.y);

        isLocked = false;
    }



    //Position
    public void SetPosition(int x, int y)
    {
        if (data == null)
        {
            data = new BlockData();
        }
        data.x = x;
        data.y = y;
    }
    //Color
    public void InitColor(int index)
    {
        if (data == null)
        {
            data = new BlockData();
        }
        data.colorIndex = index;

        this.color = ColorHelper.Instance.GetColor(data.colorIndex - 1);

        if (icon == null) { icon = GetComponentInChildren<Image>(); }
        if (icon != null) { icon.color = this.color; }

        DebugText(index);
    }
    public void ChangeColor()
    {
        data.colorIndex = ColorHelper.Instance.GetColorIndex(data.colorIndex);

        this.color = ColorHelper.Instance.GetColor(data.colorIndex - 1);

        if (icon == null) { icon = GetComponentInChildren<Image>(); }
        if (icon != null) { icon.color = this.color; }
    }
    

    //Move
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"[Block]{data.x},{data.y} : OnBeginDrag");
        if (isLocked) { return; }
        isLocked = true;
        isVerticalMove = eventData.delta.x > eventData.delta.y;
        Debug.Log($"[Block]{data.x},{data.y} : OnBeginDrag :: IsVertical ? {isVerticalMove}");
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
        Debug.Log($"[Block]{data.x},{data.y} : OnDrag");
        //if (isVerticalMove)
        //{
        //    MoveVertical(eventData.delta.y);
        //}
        //else
        //{
        //    MoveHorizontal(eventData.delta.x);
        //}
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"[Block]{data.x},{data.y} : OnDrop");
        //if (isLocked) { return; }
    }

    //Debug
    public void TestCheck()
    {
        if (icon == null) { icon = GetComponentInChildren<Image>(); }
        if (icon != null) { icon.color = Color.black; }
        txt.color = Color.white;
    }
    public void TestChangeCheck()
    {
        if (icon == null) { icon = GetComponentInChildren<Image>(); }
        if (icon != null) { icon.color = Color.gray; }
    }
}
