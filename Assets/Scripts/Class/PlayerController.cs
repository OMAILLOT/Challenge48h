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

    public void OnPlayerEnter(Case currentCase)
    {
        if (currentCase.type == TypeCase.Chance)
        {
            // Si la case est de type "Bonus", ajouter de l'argent au joueur
            currentCoin += 100;
            Debug.Log("Vous avez gagné 100 € !");
        }
/*        else if (currentCase.type == TypeCase.Malus)
        {
            // Si la case est de type "Malus", retirer de l'argent au joueur
            currentCoin -= 50;
            Debug.Log("Vous avez perdu 50 € !");
        }*/
        else if (currentCase.type == TypeCase.qiPoints)
        {
            // Si la case est de type "QI", retirer du QI au joueur
            knowledgePoint -= 50;
            Debug.Log("Vous avez perdu 50 points de QI !");
        }
        else
        {
            // Si la case est de type "Normal", ne rien faire
            Debug.Log("Case normale !");
        }
    }
}
