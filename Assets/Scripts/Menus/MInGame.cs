using System;
using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using TMPro;
using UnityEngine;

public class MInGame : MonoSingleton<MInGame>
{

    [SerializeField] private TMP_Text turn;
    [SerializeField] private TMP_Text player1QiText;
    [SerializeField] private TMP_Text player1MoneyText;
    [SerializeField] private TMP_Text player2QiText;
    [SerializeField] private TMP_Text player2MoneyText;
    [SerializeField] private TMP_Text player3QiText;
    [SerializeField] private TMP_Text player3MoneyText;
    [SerializeField] private TMP_Text player4QiText;
    [SerializeField] private TMP_Text player4MoneyText;

    private void FixedUpdate()
    {
        var players = PlayerManager.Instance.allPlayer;
        for (var i = 0; i < players.Count; i++)
        {
            switch (i)
            {
                case 0:
                    turn.text = players[i].numberOfTurn.ToString();
                    player1QiText.text = players[i].knowledgePoint.ToString();
                    player1MoneyText.text = players[i].currentCoin.ToString();
                    break;
                case 1:
                    turn.text = players[i].numberOfTurn.ToString();
                    player2QiText.text = players[i].knowledgePoint.ToString();
                    player2MoneyText.text = players[i].currentCoin.ToString();
                    break;
                case 2:
                    turn.text = players[i].numberOfTurn.ToString();
                    player3QiText.text = players[i].knowledgePoint.ToString();
                    player3MoneyText.text = players[i].currentCoin.ToString();
                    break;
                case 3:
                    turn.text = players[i].numberOfTurn.ToString();
                    player4QiText.text = players[i].knowledgePoint.ToString();
                    player4MoneyText.text = players[i].currentCoin.ToString();
                    break;
            }
        }
    }
}
