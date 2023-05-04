using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionHelper : MonoBehaviour
{
    public static PositionHelper instance { get; set; }

    [SerializeField] List<Transform> posX;
    [SerializeField] List<Transform> posY;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public Vector2 GetPos(int x, int y)
    {
        var vec2 = new Vector2();
        vec2.x = posX[x].position.x;
        vec2.y = posY[y].position.y;
        return vec2;
    }
}
