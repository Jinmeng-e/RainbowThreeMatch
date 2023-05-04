using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSet
{
    int index;
    Color color;
}

public class ColorHelper : MonoBehaviour
{
    public static ColorHelper instance;

    [SerializeField]
    List<Color> colors = null;

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
        if (colors.Count <= index)
        {
            index -= colors.Count;
        }
        return colors[index];
    }
}
