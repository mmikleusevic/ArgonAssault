using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    int _score;

    public void UpdateScore(int points)
    {
        _score += points;

        Debug.Log($"Your score is: {_score}");
    }
}
