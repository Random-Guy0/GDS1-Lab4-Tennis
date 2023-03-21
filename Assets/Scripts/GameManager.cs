using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Difficulty gameDifficulty;
    private int numPlayers;

    private void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetDifficulty(int gameDifficulty)
    {
        this.gameDifficulty = (Difficulty)gameDifficulty;
    }

    public void SetNumPlayers(int numPlayers)
    {
        this.numPlayers = numPlayers;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Main Scene":
                SetupGame();
                break;
            case "Win":
            case "GameOver":
                Destroy(gameObject);
                break;
        }
    }

    private void SetupGame()
    {
        
    }
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}
