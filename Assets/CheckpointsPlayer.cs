using System.Collections.Generic;
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
        ThatsHowWeRoll player = GetComponent<ThatsHowWeRoll>();
    }

    public void SetLastCheckPoint(Vector3 checkpointPos)
    {
        if (playerManager != null && playerIndex < playerManager.playerLastCheckPointPos.Count)
        {
            playerManager.playerLastCheckPointPos[playerIndex] = checkpointPos;
        }
    }
}