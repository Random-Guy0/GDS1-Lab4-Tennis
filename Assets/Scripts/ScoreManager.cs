using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Score playerScore;
    private Score enemyScore;
    private int playerSets;
    private int enemySets;

    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text enemyScoreText;
    [SerializeField] private TMP_Text playerSetText;
    [SerializeField] private TMP_Text enemySetText;

    private void Start()
    {
        playerScore = Score.Zero;
        enemyScore = Score.Zero;
    }

    private void UpdateText(Score updateScore, TMP_Text updateText)
    {
        switch (updateScore)
        {
            case Score.Fifteen:
                updateText.SetText("15");
                break;
            case Score.Thirty:
                updateText.SetText("30");
                break;
            case Score.Forty:
                updateText.SetText("40");
                break;
            case Score.Advantage:
                updateText.SetText("ADV");
                break;
        }
    }
    
    public void ScorePoint(bool isPlayer)
    {
        if (isPlayer)
        {
            ScorePoint(playerScore, playerScoreText, enemyScore, enemyScoreText, isPlayer);
        }
        else
        {
            ScorePoint(enemyScore, enemyScoreText, playerScore, playerScoreText, isPlayer);
        }
    }

    private void ScorePoint(Score winnerScore, TMP_Text winnerText, Score loserScore, TMP_Text loserText, bool isPlayer)
    {
        switch (winnerScore)
        {
            case Score.Advantage:
                EndSet(isPlayer);
                break;
            
            case Score.Forty:
                switch (loserScore)
                {
                    case Score.Advantage:
                        if (isPlayer)
                        {
                            enemyScore--;
                        }
                        else
                        {
                            playerScore--;
                        }
                        UpdateText(loserScore, loserText);
                        break;
                    
                    case Score.Forty:
                        if (isPlayer)
                        {
                            playerScore++;
                        }
                        else
                        {
                            enemyScore++;
                        }

                        break;
                    
                    default:
                        EndSet(isPlayer);
                        break;
                }
                break;
            
            default:
                if (isPlayer)
                {
                    playerScore++;
                }
                else
                {
                    enemyScore++;
                }
                break;
        }
        
        UpdateText(winnerScore, winnerText);
    }

    private void EndSet(bool isPlayer)
    {
        if (isPlayer)
        {
            WinSet();
        }
        else
        {
            LoseSet();
        }
        NextSet();
    }

    private void WinSet()
    {
        playerSets++;
        playerSetText.SetText(playerSets.ToString());
        if (playerSets >= 6)
        {
            //win game
        }
    }

    private void LoseSet()
    {
        enemySets++;
        enemySetText.SetText(enemySets.ToString());
        if (enemySets >= 6)
        {
            //win game
        }
    }

    private void NextSet()
    {
        playerScore = Score.Zero;
        enemyScore = Score.Zero;
        //reset game and switch server
    }
}

enum Score
{
    Zero,
    Fifteen,
    Thirty,
    Forty,
    Advantage
}
