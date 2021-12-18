using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowWinScene : MonoBehaviour
{
    public GameObject sureMessage;
   public void PlayAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        sureMessage.SetActive(true);
    }
    public void yesExit()
    {
        Application.Quit();
    }

    public void noExit()
    {
        sureMessage.SetActive(false);
    }
}
