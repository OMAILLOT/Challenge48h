using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerRenderer;

    public string playerName;
    public int currentCoin;
    public float knowledgePoint;
    public bool isMyTurn = false;
    public int playerOnCaseIndex = 0;
    public int numberOfTurn = 0;
    public Case nextCase;
    public List<Case> allCaseToNextCase = new List<Case>();

    public int totalScore;

    int diceResult;
    public void StartMyTurn()
    {
        isMyTurn = true;
        diceResult = Random.Range(PlayerManager.Instance.minDiceNumber, PlayerManager.Instance.maxDiceNumber);
        UiManager.Instance.randomNumber = diceResult;
    }

    public void PlayerMoove()
    {
        StartCoroutine(AnimationMove());
    }

    IEnumerator AnimationMove()
    {
        
        //playerOnCaseIndex++;
        int finalIndex = playerOnCaseIndex + diceResult;
        if (playerOnCaseIndex == BoardManager.Instance.allCases.Count)
        {
            playerOnCaseIndex = 0;
            finalIndex -= BoardManager.Instance.allCases.Count;
        }
        if (nextCase != null) { 
            nextCase.ResetPlayerOnCase(this); 
        }
        while (playerOnCaseIndex < finalIndex)
        {
            nextCase = BoardManager.Instance.allCases[playerOnCaseIndex];   
            nextCase.PlayerOnthisCase(this, false);
            yield return DOTween.Sequence().Append(transform.DOMove(BoardManager.Instance.allCases[playerOnCaseIndex].transform.position + Vector3.up * 1, .25f))
                .Join(playerRenderer.transform.DOLocalMoveY(2, .125f))
                .Append(playerRenderer.transform.DOLocalMoveY(0, .125f))
                .WaitForCompletion();
            if (playerOnCaseIndex < finalIndex-1) nextCase.ResetPlayerOnCase(this);

            playerOnCaseIndex++;
            if (playerOnCaseIndex >= BoardManager.Instance.allCases.Count)
            {
                playerOnCaseIndex = 0;
                finalIndex -= BoardManager.Instance.allCases.Count;
                numberOfTurn++;
                if (numberOfTurn == PlayerManager.Instance.numberOfTurn)
                {
                    PlayerManager.Instance.PlayerFinish();
                    nextCase.ResetPlayerOnCase(this);
                    break;
                }
            }
        }
        if (numberOfTurn < PlayerManager.Instance.numberOfTurn)
        {
            nextCase.ResetPlayerOnCase(this);
            nextCase.PlayerOnthisCase(this, true, true);
        }
        //transform.position = nextcase.transform.position + Vector3.up * 1;
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
