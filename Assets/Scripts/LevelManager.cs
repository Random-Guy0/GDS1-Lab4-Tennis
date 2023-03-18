using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void SelectLevel()
    {
        SceneManager.LoadScene("Main Scene");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
   
}
