using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    string strTimer = "Time : ";
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Game.instance.ShowTimer += ShowTimer;
    }

    public void ShowTimer(int time)
    {
        //Debug.Log($"Show Timer : {time}");
        text.text = strTimer + time;
    }
}
