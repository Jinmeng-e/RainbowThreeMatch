using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    string strScore = "Score : ";
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Game.instance.ShowScore += ShowScore;
    }

    public void ShowScore(int score)
    {
        text.text = strScore + score;
    }
}
