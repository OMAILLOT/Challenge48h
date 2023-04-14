using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionCard", menuName = "Card/QuestionCard", order = 1)]
public class CardQuestion : ScriptableObject
{
    public string question;
    public List<string> reponse;
    public int CoinUpdate;
    public int knowledgeUpdate;

   // public int answerIndex;

    public string bonneReponse;
    //public List<QuestionData> questions;

    public void WinChoose()
    {
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].currentCoin += CoinUpdate;
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint += knowledgeUpdate;
    }

    public void looseChose()
    {
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].currentCoin -= CoinUpdate;
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint -= knowledgeUpdate;
    }

}

