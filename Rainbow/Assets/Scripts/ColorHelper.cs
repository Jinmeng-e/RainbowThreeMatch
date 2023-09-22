using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper : MonoBehaviour
{
    public static ColorHelper Instance => instance;

    [SerializeField]
    List<Color> colors = null;
    static ColorHelper instance;
    public bool IsColorFilled => colors.Count >= Game.instance.MaxColorCount;

    [SerializeField] Sprite[] sprites;



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(colors == null || colors.Count <= 0)
        {
            colors = new List<Color>();

            var orange = Color.white;
            var purple = Color.white;

            ColorUtility.TryParseHtmlString("#FFA500", out orange);
            ColorUtility.TryParseHtmlString("#B711FF", out purple);

            colors.Add(Color.red);
            colors.Add(orange);
            colors.Add(Color.yellow);
            colors.Add(Color.green);
            colors.Add(Color.blue);
            colors.Add(Color.magenta);
            colors.Add(purple);
        }
    }

    public Color GetColor(int index)
    {
        if(Game.instance.MaxColorCount < index)
        {
            index -= Game.instance.MaxColorCount;
        }
        return colors[index];
    }
    public Sprite GetSprite(int index)
    {
        if(Game.instance.MaxColorCount < index)
        {
            index -= Game.instance.MaxColorCount;
        }
        return sprites[index];
    }
    public int GetColorIndex(int index)
    {
        if (Game.instance.MaxColorCount < index)
        {
            index -= Game.instance.MaxColorCount;
        }
        return index;
    }
}