using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public List<PlayerController> allPlayer;
    public List<Transform> playerStartAndEnd;
    public int minDiceNumber;
    public int maxDiceNumber;
    public int currentIndexPlayer;
    [SerializeField] int startPlayerCoin;
    [SerializeField] int startKnowledgePoint;
    public int numberOfTurn;

    List<string> playerNameForCamera = new List<string>();

    public void Start()
    {
        for (int i = 0; i < allPlayer.Count; i++) 
        {
            playerNameForCamera.Add($"Player{i+1}");
            allPlayer[i].transform.position = playerStartAndEnd[i].position;
            allPlayer[i].currentCoin = startPlayerCoin;
            allPlayer[i].knowledgePoint = startKnowledgePoint;
        }
        StartFirstTurn();
    }

    public void StartFirstTurn()
    {
        currentIndexPlayer = Random.Range(0, allPlayer.Count);
        allPlayer[currentIndexPlayer].StartMyTurn();
        UiManager.Instance.StartTurnUI();
        CameraManager.Instance.CameraSwitch(playerNameForCamera[currentIndexPlayer]);

    }
    public void NextTurn()
    {
        allPlayer[currentIndexPlayer].isMyTurn = false;

        currentIndexPlayer++;
        if (currentIndexPlayer >= allPlayer.Count) currentIndexPlayer = 0;

        CameraManager.Instance.CameraSwitch(playerNameForCamera[currentIndexPlayer]);
        allPlayer[currentIndexPlayer].StartMyTurn();
        UiManager.Instance.StartTurnUI();
    }

    public void EndGame()
    {

    }

    public void MooveCurrentPlayer()
    {
        foreach (PlayerController player in allPlayer)
        {
            if (player.isMyTurn)
            {
                player.PlayerMoove();
            }
        }
    }

    public void PlayerFinish()
    {
        int index = currentIndexPlayer;
        allPlayer[index].transform.position = playerStartAndEnd[index].position;
        playerStartAndEnd.RemoveAt(index);
        allPlayer.RemoveAt(index);
        CameraManager.Instance.allCameras.RemoveAt(index);
        if (allPlayer.Count <= 0)
        {
            EndGame();
        }
    }
}
