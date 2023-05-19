using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionHelper : MonoBehaviour
{
    public static PositionHelper instance { get; set; }
    public float vHarf = 0;
    public float hHarf = 0;

    public int YCount => posH.Count;
    public int XCount => posV.Count;
    [SerializeField] List<Transform> posV;
    [SerializeField] List<Transform> posH;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        //vHarf = posV[0].position.x - posV[1].position.x;
        //hHarf = posH[0].position.y - posH[1].position.y;
    }

    public Vector2 GetPos(int x, int y)
    {
        Debug.Log($"[POSITION HELPER] : Get Position :: [{x},{y}]");
        var vec2 = new Vector2();
        vec2.x = posV[x].position.x;
        vec2.y = posH[y].position.y;
        return vec2;
    }
}
