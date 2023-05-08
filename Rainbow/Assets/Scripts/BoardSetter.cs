using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSetter : MonoBehaviour
{
    [SerializeField] int vCount;
    [SerializeField] int hCount;
    [SerializeField] float space;
    float boardWidth = 0;
    float boardHeight = 0;
    float blockWidth;
    float blockHeight;

    private void Awake()
    {
        if(boardWidth == 0)
        {
            if(Screen.width > Screen.height)
            {
                boardWidth = Screen.height * .9f;
                boardHeight = Screen.height * .9f;
            }
            else
            {
                boardWidth = Screen.width * .9f;
                boardHeight = Screen.width * .9f;
            }
        }

        blockWidth = boardWidth - (space * (vCount - 1)) / vCount;
        blockHeight = boardHeight - (space * (hCount - 1)) / hCount;
    }
}
