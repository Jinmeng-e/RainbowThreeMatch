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
        text.text = $"{strTimer}{99}";
    }

    public void ShowTimer(int time)
    {
        //Debug.Log($"Show Timer : {time}");
        text.text = $"{strTimer}{time}";
    }
}
