using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public List<PlayerController> allPlayer;
    public int minDiceNumber;
    public int maxDiceNumber;
    public int currentIndexPlayer;
    [SerializeField] int startPlayerCoin;
    [SerializeField] int startKnowledgePoint;

    public void Start()
    {
        foreach(PlayerController player in allPlayer)
        {
            player.currentCoin = startPlayerCoin;
            player.knowledgePoint = startKnowledgePoint;
        }
        StartFirstTurn();
    }

    public void StartFirstTurn()
    {
        currentIndexPlayer = Random.Range(0, allPlayer.Count);
        allPlayer[currentIndexPlayer].StartMyTurn();
        UiManager.Instance.StartTurnUI();

    }
    public void NextTurn()
    {
        allPlayer[currentIndexPlayer].isMyTurn = false;
        currentIndexPlayer++;
        if (currentIndexPlayer >= allPlayer.Count) currentIndexPlayer = 0;


        allPlayer[currentIndexPlayer].StartMyTurn();
        UiManager.Instance.StartTurnUI();
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
}
