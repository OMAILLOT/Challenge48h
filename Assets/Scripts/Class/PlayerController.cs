using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string playerName;
    public int currentCoin;
    public float knowledgePoint;
    public bool isMyTurn = false;
    public int playerOnCaseIndex = 0;
    public int numberOfTurn = 0;
    public Case nextcase;

    int diceResult;
    public void StartMyTurn()
    {
        isMyTurn = true;
        diceResult = Random.Range(2, 12);
        UiManager.Instance.randomNumber = diceResult;
    }

    public void PlayerMoove()
    {
        playerOnCaseIndex += diceResult;
        if (playerOnCaseIndex >= BoardManager.Instance.allCases.Count)
        {
            numberOfTurn++;
            playerOnCaseIndex = playerOnCaseIndex - BoardManager.Instance.allCases.Count;
        }

        if (nextcase != null)
        {
            nextcase.ResetPlayerOnCase(this);
        }
        nextcase = BoardManager.Instance.allCases[playerOnCaseIndex];
        transform.position = nextcase.transform.position + Vector3.up * 1;
        nextcase.OnStartCase(this);
    }
}
