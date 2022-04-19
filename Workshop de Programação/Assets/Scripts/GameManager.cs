using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private int score;

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
