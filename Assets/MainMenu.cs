using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        PlayerManager.instance.ClearPlayers();
        Global.finish = false;
        SceneManager.LoadScene("SampleScene");
    }

    public void DoSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
