using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    int _score;
    TMP_Text _scoreText;

    void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    public void UpdateScore(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString();
    }
}
