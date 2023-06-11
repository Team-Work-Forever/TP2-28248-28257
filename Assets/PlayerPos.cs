using UnityEngine;
using System.Collections.Generic;

public class PlayerPos : MonoBehaviour
{
    public List<ThatsHowWeRoll> players;
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = PlayerManager.instance;

        for (int i = 0; i < players.Count; i++)
        {
            ThatsHowWeRoll player = players[i];

            int playerIndex = playerManager.players.IndexOf(player);
            if (playerIndex >= 0 && playerIndex < playerManager.playerLastCheckPointPos.Count)
            {
                player.transform.position = playerManager.playerLastCheckPointPos[playerIndex];
            }
        }
    }

    void Update()
    {
        RespawnManual();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Respawn"))
    //     {
    //         for (int i = 0; i < players.Count; i++)
    //         {
    //             ThatsHowWeRoll player = players[i];
    //             int playerIndex = playerManager.players.IndexOf(player);
    //             player.transform.position = playerManager.playerLastCheckPointPos[playerIndex];
    //         }
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            ThatsHowWeRoll player = gameObject.GetComponent<ThatsHowWeRoll>();
            if (player != null && players.Contains(player))
            {
                int playerIndex = playerManager.players.IndexOf(player);
                player.transform.position = playerManager.playerLastCheckPointPos[playerIndex];
            }
        }
    }



    private void RespawnManual()
    {
        for (int i = 0; i < players.Count; i++)
        {
            ThatsHowWeRoll player = players[i];
            int playerIndex = playerManager.players.IndexOf(player);

            if (playerIndex == 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    player.transform.position = playerManager.playerLastCheckPointPos[playerIndex];
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.RightShift))
                {
                    player.transform.position = playerManager.playerLastCheckPointPos[playerIndex];
                }
            }
        }
    }
}
