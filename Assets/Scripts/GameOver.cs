 using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public void setup()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Level1");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menue");
    }
}
