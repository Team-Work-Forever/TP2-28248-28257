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

    /// <summary>
    /// Create players
    /// </summary>
    /// <param name="player"></param>
    /// <param name="initialPosition"></param>
    public void RegisterPlayer(ThatsHowWeRoll player, Vector3 initialPosition)
    {
        players.Add(player);
        playerTransformList.Add(player.transform);
        playerLastCheckPointPos.Add(initialPosition);
    }

    /// <summary>
    /// Manage the last checkpoint of the player
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="checkpointPos"></param>
    public void SetLastCheckPointPosition(int playerIndex, Vector3 checkpointPos)
    {
        if (playerIndex >= 0 && playerIndex < playerLastCheckPointPos.Count)
        {
            playerLastCheckPointPos[playerIndex] = checkpointPos;
        }
    }

    /// <summary>
    /// Clear players
    /// </summary>
    public void ClearPlayers()
    {
        players.Clear();
        playerTransformList.Clear();
        playerLastCheckPointPos.Clear();
    }
}