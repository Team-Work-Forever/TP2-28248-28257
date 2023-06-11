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

    void Update()
    {
        if (Global.finish)
        {
            FindObjectOfType<AudioManager>().Play("Win");
            countdownTimer.FinishGame();
        }
    }

    public void SetCountdownTimer(CountdownTimer timer)
    {
        countdownTimer = timer;
    }

    public void SetPlayer(ThatsHowWeRoll currentPlayer)
    {
        player = currentPlayer;
        checkpointsPlayer = currentPlayer.GetComponentInChildren<CheckpointsPlayer>();
    }

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

    public void CheckWinner(ThatsHowWeRoll winnerPlayer)
    {
        Global.finish = true;
        Global.winnerPlayer = winnerPlayer;

        countdownTimer.FinishGame();
    }
}
