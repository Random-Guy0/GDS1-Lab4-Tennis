using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject ball;
    [SerializeField] private BallClone ballController;
    [SerializeField] private Vector2 playAreaMax;
    [SerializeField] private Vector2 playAreaMin;
    [SerializeField] private float netZPos;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    private Vector3 initialPlayerPos;
    private Vector3 initialEnemyPos;
    private Vector3 initialBallPos;

    private void Start()
    {
        playerScore = Score.Zero;
        enemyScore = Score.Zero;

        initialPlayerPos = player.transform.position;
        initialEnemyPos = enemy.transform.position;
        initialBallPos = ball.transform.position;
    }

    private void UpdateText(Score updateScore, TMP_Text updateText)
    {
        switch (updateScore)
        {
            case Score.Zero:
                updateText.SetText("0");
                break;
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
            ScorePoint(playerScore, enemyScore, isPlayer);
        }
        else
        {
            ScorePoint(enemyScore, playerScore, isPlayer);
        }
    }

    private void ScorePoint(Score winnerScore, Score loserScore, bool isPlayer)
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
            SceneManager.LoadScene("Win");
        }
    }

    private void LoseSet()
    {
        enemySets++;
        enemySetText.SetText(enemySets.ToString());
        if (enemySets >= 6)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    private void NextSet()
    {
        playerScore = Score.Zero;
        enemyScore = Score.Zero;
        UpdateText(playerScore, playerScoreText);
        UpdateText(enemyScore, enemyScoreText);
        //reset game and switch server
    }

    private void Update()
    {
        Vector3 ballPos = ball.transform.position;

        if (ballPos.x < playAreaMin.x || ballPos.z < playAreaMin.y || ballPos.x > playAreaMax.x ||
            ballPos.z > playAreaMax.y || ballController.GetBounces() >= 2)
        {

            bool isPlayer = ballPos.z > netZPos;
            ScorePoint(isPlayer);
            UpdateText(playerScore, playerScoreText);
            UpdateText(enemyScore, enemyScoreText);

            player.transform.position = initialPlayerPos;
            enemy.transform.position = initialEnemyPos;
            ball.transform.position = initialBallPos;
            ballController.SetVelocity(Vector3.zero);
            ballController.SetPlayerLastHit(null);
        }
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
