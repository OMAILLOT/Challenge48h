using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionCard", menuName = "Card/QuestionCard", order = 1)]
public class CardQuestion : ScriptableObject
{
    public string question;
    public List<string> reponses;
    public int CoinUpdate;
    public int knowledgeUpdate;

    public int answerIndex;

    public void WinChoose()
    {
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].currentCoin += CoinUpdate;
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint += knowledgeUpdate;
    }


}
