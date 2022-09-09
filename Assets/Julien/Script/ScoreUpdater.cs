using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    private TMP_Text _text;
    [SerializeField] private List<GameObject> _scoreIcons;

    [SerializeField] private TMP_Text _timer;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void DisplayScore(int score)
    {
        _text.text = $"Score : {score}";
    }

    public void UpdateScoreGame1(int score)
    {
        if (score > 0)
        {
            var scoreImage = _scoreIcons[score - 1].GetComponent<SpriteRenderer>();
            scoreImage.color = new Color(scoreImage.color.r, scoreImage.color.g, scoreImage.color.b, scoreImage.color.a == 1 ? .3f : 1f);
        }
    }

    public void UpdateTimer(int timer)
    {
        _timer.text = $"{timer}";
    }
}
