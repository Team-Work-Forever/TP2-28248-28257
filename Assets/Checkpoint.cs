using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Checkpoints checkpoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThatsHowWeRoll player = other.GetComponentInParent<ThatsHowWeRoll>();

            int playerIndex = PlayerManager.instance.players.IndexOf(player);
            Vector3 checkpointPos = transform.position;

            PlayerManager.instance.SetLastCheckPointPosition(playerIndex, checkpointPos);
            checkpoints.PlayerThroughCheckpoint(this, other.transform, playerIndex);
        }
    }


    public void SetTrackCheckpoints(Checkpoints checkpoints)
    {
        this.checkpoints = checkpoints;
    }
}