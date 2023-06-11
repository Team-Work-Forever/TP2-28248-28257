using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private List<Transform> playerTransformList;
    private List<Checkpoint> checkpointsList;
    private List<int> nextCheckpointIndexList;

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

    public void PlayerThroughCheckpoint(Checkpoint checkpoint, Transform player, int playerIndex)
    {
        int checkpointIndex = checkpointsList.IndexOf(checkpoint);
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