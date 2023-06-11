using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public GameObject player;

    public int countdownTime;
    public Text countdownDisplay;

    public Text lapsDisplay;
    public Text checkpointDisplay;
    public Text statusText;
    public Text velocity;
    public Button initialMenu;

    public GameObject textInfo;

    private ThatsHowWeRoll playerInputs;

    /// <summary>
    /// Block the movements of the players in the begin of the game and begin the countdown
    /// </summary>
    public void Start()
    {
        playerInputs = player.GetComponent<ThatsHowWeRoll>();

        if (playerInputs != null)
        {
            playerInputs.enabled = false;
        }

        StartCoroutine(CountdownOnStart());
    }

    /// <summary>
    /// Countdown Timer and manage of the player interface
    /// </summary>
    /// <returns></returns>
    IEnumerator CountdownOnStart()
    {
        while (countdownTime > 0)
        {
            statusText.gameObject.SetActive(false);
            initialMenu.gameObject.SetActive(false);
            lapsDisplay.gameObject.SetActive(false);
            checkpointDisplay.gameObject.SetActive(false);
            textInfo.SetActive(false);
            velocity.gameObject.SetActive(false);

            countdownDisplay.text = countdownTime.ToString();

            FindObjectOfType<AudioManager>().Play("CountdownTimer");

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        FindObjectOfType<AudioManager>().Play("GO");
        countdownDisplay.text = "GO!";

        playerInputs.enabled = true;

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);

        textInfo.SetActive(true);
        lapsDisplay.gameObject.SetActive(true);
        velocity.gameObject.SetActive(true);
        checkpointDisplay.gameObject.SetActive(true);
    }

    /// <summary>
    /// Go to Initial Menu
    /// </summary>
    public void InitialMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Manage of interface when one player win the game and block the movement of the players
    /// </summary>
    public void FinishGame()
    {
        if (playerInputs == Global.winnerPlayer)
        {
            statusText.text = "Victory";
            statusText.gameObject.SetActive(true);
            initialMenu.gameObject.SetActive(true);
        }
        else
        {
            statusText.text = "Defeat";
            statusText.gameObject.SetActive(true);
            initialMenu.gameObject.SetActive(true);
        }

        playerInputs.enabled = false;

        lapsDisplay.gameObject.SetActive(false);
        velocity.gameObject.SetActive(false);
        checkpointDisplay.gameObject.SetActive(false);
        textInfo.SetActive(false);
    }
}
