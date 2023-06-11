using UnityEngine;

public class CheckpointsPlayer : MonoBehaviour
{
    public int numCheckpoints;
    public int laps;

    public ThatsHowWeRoll carController;

    private PlayerManager playerManager;
    private int playerIndex;

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    /// <summary>
    /// Define last checkpoint
    /// </summary>
    /// <param name="checkpointPos"></param>
    public void SetLastCheckPoint(Vector3 checkpointPos)
    {
        if (playerManager != null && playerIndex < playerManager.playerLastCheckPointPos.Count)
        {
            playerManager.playerLastCheckPointPos[playerIndex] = checkpointPos;
        }
    }
}