using alexshkorp.bumpcars.Multiplayer;
using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UIScore : MonoBehaviour
{
    [Tooltip("text of the score of players")]
    [SerializeField] TMP_Text[] txtPlayers;

    [Inject]
    NetworkRunner _runner;

    private void Start()
    {
        GameStats.ActionScoreChange += UpdateUIScore;
        SetInitScore();
    }

    private void OnDestroy() => GameStats.ActionScoreChange -= UpdateUIScore;

    /// <summary>
    /// Update the score of all players
    /// </summary>
    /// <param name="scores"></param>
    private void UpdateUIScore(NetworkDictionary<PlayerRef, int> scores)
    {
        int place = 0;
        foreach (var player in _runner.ActivePlayers)
        {
            int score = 0;
            if (scores.ContainsKey(player))
            {
                score = scores[player];
            }
            txtPlayers[place].text = score.ToString();
            place++;
        }
    }
    private void SetInitScore()
    {
        foreach (var txtScore in txtPlayers)
        {
            txtScore.text = "0";
        }
    }

}
