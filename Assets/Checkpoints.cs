using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private List<Transform> playerTransformList;
    private List<Checkpoint> checkpointsList;
    private List<int> nextCheckpointIndexList;

    /// <summary>
    /// Define all checkpoints of the track and players
    /// </summary>
    private void Awake()
    {
        Transform checkpoints = transform.Find("CheckPoints");

        checkpointsList = new List<Checkpoint>();
        foreach (Transform checkpoint in checkpoints)
        {
            Checkpoint checkpointSingle = checkpoint.GetComponent<Checkpoint>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointsList.Add(checkpointSingle);
        }

        FillPlayerTransformList();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FillPlayerTransformList();
    }

    /// <summary>
    /// Define all players and theirs next checkpoint
    /// </summary>
    private void FillPlayerTransformList()
    {
        playerTransformList = new List<Transform>();

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            ThatsHowWeRoll player = playerObject.GetComponent<ThatsHowWeRoll>();
            if (player != null)
            {
                playerTransformList.Add(player.transform);
            }
        }

        nextCheckpointIndexList = new List<int>();
        for (int i = 0; i < playerTransformList.Count; i++)
        {
            nextCheckpointIndexList.Add(0);
        }
    }

    /// <summary>
    /// Change the next checkpoint of the player if he pass the correct one
    /// </summary>
    /// <param name="checkpoint"></param>
    /// <param name="player"></param>
    /// <param name="playerIndex"></param>
    public void PlayerThroughCheckpoint(Checkpoint checkpoint, Transform player, int playerIndex)
    {
        int nextCheckpointIndex = nextCheckpointIndexList[playerIndex];
        if (checkpointsList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            nextCheckpointIndexList[playerIndex] = (nextCheckpointIndex + 1) % checkpointsList.Count;
            CheckpointsPlayer checkpointsPlayer = player.GetComponent<CheckpointsPlayer>();
            checkpointsPlayer.numCheckpoints++;

            if (nextCheckpointIndex == 0)
            {
                checkpointsPlayer.laps++;
                checkpointsPlayer.numCheckpoints = 0;
                checkpointsPlayer.numCheckpoints++;
            }
        }
        else
        {
            Debug.Log("Checkpoint Errado!");
        }
    }
}