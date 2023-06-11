using UnityEngine;
using UnityEngine.UI;

public class UIInterface : MonoBehaviour
{
    public CountdownTimer countdownTimer;
    public Text lapsDisplay;
    public Text checkpointDisplay;
    public Text velocityDisplay;

    public ThatsHowWeRoll player;
    private CheckpointsPlayer checkpointsPlayer;

    void Start()
    {
        UpdateInfo();
    }

    /// <summary>
    /// Verify if game is finish
    /// </summary>
    void Update()
    {
        if (Global.finish)
        {
            FindObjectOfType<AudioManager>().Play("Win");
            countdownTimer.FinishGame();
        }
    }

    /// <summary>
    /// Give the possibility of change counter time
    /// </summary>
    /// <param name="timer"></param>
    public void SetCountdownTimer(CountdownTimer timer)
    {
        countdownTimer = timer;
    }

    /// <summary>
    /// Define player to UI
    /// </summary>
    /// <param name="currentPlayer"></param>
    public void SetPlayer(ThatsHowWeRoll currentPlayer)
    {
        player = currentPlayer;
        checkpointsPlayer = currentPlayer.GetComponentInChildren<CheckpointsPlayer>();
    }

    /// <summary>
    /// Present the info of the player and when the laps of the player are equal to the number of laps needed finish the game
    /// </summary>
    public void UpdateInfo()
    {
        velocityDisplay.text = player.velocity.ToString();

        if (countdownTimer != null && lapsDisplay != null && checkpointDisplay != null && checkpointsPlayer != null)
        {
            if (checkpointsPlayer.laps == -1)
            {
                lapsDisplay.text = "0 / " + Global.laps.ToString();
            }
            else
            {
                lapsDisplay.text = checkpointsPlayer.laps.ToString() + " / " + Global.laps.ToString();
            }
            checkpointDisplay.text = checkpointsPlayer.numCheckpoints.ToString() + " / " + Global.checkpoints.ToString();
        }

        if (checkpointsPlayer != null && checkpointsPlayer.laps == Global.neededLaps)
        {
            CheckWinner(player);
        }
    }

    /// <summary>
    /// Finish the game
    /// </summary>
    /// <param name="winnerPlayer"></param>
    public void CheckWinner(ThatsHowWeRoll winnerPlayer)
    {
        Global.finish = true;
        Global.winnerPlayer = winnerPlayer;

        countdownTimer.FinishGame();
    }
}
