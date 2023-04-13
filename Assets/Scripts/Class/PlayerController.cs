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
    public Case nextcase;
    public List<Case> allCaseToNextCase = new List<Case>();

    int diceResult;
    int nextCase;
    Sequence playerMooveSequence;
    public void StartMyTurn()
    {
        isMyTurn = true;
        diceResult = Random.Range(PlayerManager.Instance.minDiceNumber, PlayerManager.Instance.maxDiceNumber);
        UiManager.Instance.randomNumber = diceResult;
        playerMooveSequence = DOTween.Sequence();
    }

    public void PlayerMoove()
    {

        StartCoroutine(AnimationMove());
            //.OnComplete(PlayerMooveInCase);
            /*.Join(playerRenderer.transform.DOLocalMoveY(2, .5f))
            .Append(playerRenderer.transform.DOLocalMoveY(0, .5f)).OnStepComplete(() => nextCase++).SetLoops(diceResult).OnComplete(PlayerMooveInCase);*/
    }

    IEnumerator AnimationMove()
    {
        nextCase = playerOnCaseIndex + 1;
        int finalIndex = playerOnCaseIndex + diceResult + 1;
        if (nextCase == BoardManager.Instance.allCases.Count)
        {
            nextCase = 0;
            finalIndex -= BoardManager.Instance.allCases.Count;
        }
        while (nextCase < finalIndex)
        {
            yield return DOTween.Sequence().Append(transform.DOMove(BoardManager.Instance.allCases[nextCase].transform.position + Vector3.up * 1, .25f))
                .Join(playerRenderer.transform.DOLocalMoveY(2, .125f))
                .Append(playerRenderer.transform.DOLocalMoveY(0, .125f))
                .WaitForCompletion();
            nextCase++;
            if (nextCase >= BoardManager.Instance.allCases.Count)
            {
                nextCase = 0;
                finalIndex -= BoardManager.Instance.allCases.Count;
            }
        }
        PlayerMooveInCase();
    }

    private void PlayerMooveInCase()
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
        //transform.position = nextcase.transform.position + Vector3.up * 1;
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
