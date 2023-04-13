using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public List<PlayerController> allPlayer;
    [SerializeField] int startPlayerCoin;
    [SerializeField] int startAverage;

    public void Start()
    {
        foreach(PlayerController player in allPlayer)
        {
            player.currentCoin = startPlayerCoin;
            player.average = startAverage;
        }
        StartFirstTurn();
    }

    public void StartFirstTurn()
    {
        allPlayer[Random.Range(0, allPlayer.Count)].StartMyTurn();

    }
    public void StartTurn()
    {

    }
}
