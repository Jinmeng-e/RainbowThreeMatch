using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] Image icon;
    [SerializeField] AnimationBlock anim;
    bool isVerticalMove = false;
    float addedMoveValue = 0f;
    float lastMoveValue = 0f;
    public Color Color => color;
    public Sprite Sprite => icon.sprite;
    public int colorIndex => data.colorIndex;

    Color color = Color.white;
    BlockData data;
    bool isLocked = false;
    int height = 0;


    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        icon = transform.GetChild(0).GetComponent<Image>();
        anim = icon.gameObject.AddComponent<AnimationBlock>();
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
    public void ChangeSprite()
    {
        anim.ChangeBlock();
        data.colorIndex = ColorHelper.Instance.GetColorIndex(data.colorIndex);
        this.color = ColorHelper.Instance.GetColor(data.colorIndex - 1);

        if (icon == null)
        {
            icon = transform.GetChild(0).GetComponent<Image>();
        }
        icon.sprite = ColorHelper.Instance.GetSprite(data.colorIndex-1);
    }
    public void Pop()
    {
        data.colorIndex = 0;
        anim.Pop(this.color);
    }
    public void Show(int height)
    {
        //Debug.Log($"[Block] : Show : {gameObject.name} ,HEIGHT:: {height}");

        this.height = height;

        this.color = ColorHelper.Instance.GetColor(data.colorIndex - 1);

        if (icon == null)
        {
            icon = transform.GetChild(0).GetComponent<Image>();
        }
        icon.sprite = ColorHelper.Instance.GetSprite(data.colorIndex - 1);

        // 올려서 애니메이션 시작
        anim.SetDrop(this.height);
        anim.Drop(this.height);
    }
    public bool CheckDropEnd()
    {
        Debug.Log($"CheckDropEnd :: {transform.parent.parent.name} {transform.parent.name} {anim.PosY}");
        return anim.PosY <= 0;
    }


    //Debug
    public void TestCheck()
    {
        icon.color = Color.black;
    }
    public void TestChangeCheck()
    {
        icon.color = Color.gray;
        anim.Pop(this.color);
    }
}