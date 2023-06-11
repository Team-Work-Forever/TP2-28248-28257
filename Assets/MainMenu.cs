using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    /// <summary>
    /// Start Game
    /// </summary>
    public void PlayGame()
    {
        PlayerManager.instance.ClearPlayers();
        Global.finish = false;
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Make button sound
    /// </summary>
    public void DoSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    /// <summary>
    /// Exit Game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

}
