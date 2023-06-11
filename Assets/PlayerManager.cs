using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public List<ThatsHowWeRoll> players;
    public List<Vector3> playerLastCheckPointPos;
    public List<Transform> playerTransformList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        players = new List<ThatsHowWeRoll>();
        playerTransformList = new List<Transform>();
        playerLastCheckPointPos = new List<Vector3>();
    }

    public void RegisterPlayer(ThatsHowWeRoll player, Vector3 initialPosition)
    {
        players.Add(player);
        playerTransformList.Add(player.transform);
        playerLastCheckPointPos.Add(initialPosition);
    }

    public void SetLastCheckPointPosition(int playerIndex, Vector3 checkpointPos)
    {
        if (playerIndex >= 0 && playerIndex < playerLastCheckPointPos.Count)
        {
            playerLastCheckPointPos[playerIndex] = checkpointPos;
        }
    }

    public void ClearPlayers()
    {
        players.Clear();
        playerTransformList.Clear();
        playerLastCheckPointPos.Clear();
    }

    public int GetNextPlayerID()
    {
        int playerID = players.Count;
        return playerID;
    }
}