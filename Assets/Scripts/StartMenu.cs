using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AllControl;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
