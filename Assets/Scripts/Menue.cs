using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Menue : MonoBehaviour
{

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

  

    public void StartAgain()
    {
       

        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");

    }

    public void StartMenue()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menue");

    }


    public void Skipe()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }


    public void Exite()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Exite");
    }

    public void gameExite()
    {
        Application.Quit();
       
    }


    public void winner()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("success");
    }
}
