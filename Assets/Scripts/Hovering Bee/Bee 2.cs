using UnityEngine;
using UnityEngine.SceneManagement;

public class Bee2 : MonoBehaviour
{

    public Transform player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            //Debug.Log("Game Over: Enemy touched player!");
            Invoke("GameOver", 0.5f); // Delay of 0.5 seconds
        }
    }

    void GameOver()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Over");

    }

}
