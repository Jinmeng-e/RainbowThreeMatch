using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Block : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] Button btn;
    [SerializeField] string strDropAnim = "drop";
    [SerializeField] string strHeightAnim = "height";
    [SerializeField] string strDroppedAnim = "dropped";
    [SerializeField] string strPopAnim = "pop";
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI txt;// 임시 
    bool isVerticalMove = false;
    float addedMoveValue = 0f;
    float lastMoveValue = 0f;
    public Color Color => color;
    public Sprite Sprite => icon.sprite;
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


    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }


    void onClick()
    {
        Debug.Log($"[BLOCK] : onclick :: {data.x},{data.y}");
        Game.instance.Check((data.x, data.y));
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
    public void InitColorData(int index)
    {
        if (data == null)
        {
            data = new BlockData();
        }
        data.colorIndex = index;
    }
    //public void ChangeColor()
    //{
    //    data.colorIndex = ColorHelper.Instance.GetColorIndex(data.colorIndex);

    //    this.color = ColorHelper.Instance.GetColor(data.colorIndex - 1);

    //    if (icon == null) { icon = transform.GetChild(0).GetComponent<Image>(); }
    //    if (icon != null) { icon.color = this.color; }
    //    DebugText(data.colorIndex);
    //}
    public void ChangeSprite()
    {
        data.colorIndex = ColorHelper.Instance.GetColorIndex(data.colorIndex);

        if (icon == null)
        {
            icon = transform.GetChild(0).GetComponent<Image>();
        }
        icon.sprite = ColorHelper.Instance.GetSprite(data.colorIndex-1);
    }
    internal void Pop()
    {
        data.colorIndex = 0;
        anim.SetFloat(strHeightAnim, 0);
        anim.SetTrigger(strPopAnim);
    }

    //Debug
    public void TestCheck()
    {
        if (icon == null) { icon = transform.GetChild(0).GetComponent<Image>(); }
        if (icon != null) { icon.color = Color.black; }
        txt.color = Color.white;
    }
    public void TestChangeCheck()
    {
        if (icon == null) { icon = transform.GetChild(0).GetComponent<Image>(); }
        if (icon != null) { icon.color = Color.gray; }
    }
    public void Show(int height)
    {
        //Debug.Log($"[Block] : Show : {gameObject.name} ,HEIGHT:: {height}");
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if(anim != null && height > 0)
        {
            StartCoroutine(IEDrop(height));
        }
    }

    IEnumerator IEDrop(int height)
    {
        // 1 - 7  // 0.875 - 0.125
        // set height
        var dropTime = Game.instance.DropTime;
        var HValue = (float)(8 - height) * 0.125f;
        anim.SetFloat(strHeightAnim, HValue);
        yield return null;

        anim.SetTrigger(strDropAnim);

        // set color
        //this.color = ColorHelper.Instance.GetColor(data.colorIndex - 1);

        //if (icon == null) { icon = transform.GetChild(0).GetComponent<Image>(); }
        //if (icon != null) { icon.color = this.color; }

        // set color
        this.color = ColorHelper.Instance.GetColor(data.colorIndex - 1);

        if (icon == null)
        {
            icon = transform.GetChild(0).GetComponent<Image>();
        }
        icon.sprite = ColorHelper.Instance.GetSprite(data.colorIndex - 1);
        //if (icon != null) { icon.color = this.color; }

        // set text
        DebugText(data.colorIndex);

        // animation
        float timeValue = HValue * dropTime;
        //Debug.Log($"[Block] : HEIGHT :: {height} :: HVALUE :: {HValue} :: timevalue {timeValue}");

        while (dropTime > timeValue)
        {
            anim.SetFloat(strHeightAnim, timeValue/dropTime);
            timeValue += Time.deltaTime;
            if(timeValue >= dropTime) { timeValue = dropTime; }
            yield return null;
        }
        anim.SetTrigger(strDroppedAnim);
        yield return null;
    }
}