using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Card", menuName = "Card/EvenmentCard", order = 1)]
public class EvenmentCard : ScriptableObject
{
    public string Description;

    public int CoinUpdate;
    public int knowledgeUpdate;
    public int coinCondition;
    public int knowledgeCondition;
    [Space(5)]
    bool isConditionCoin;
    bool isConditionKnowledge;
    [Space(5)]
    bool isSuperior;

    public void ActiveEvenmentCard()
    {
        if (isConditionCoin)
        {
            foreach(PlayerController player in  PlayerManager.Instance.allPlayer)
            {
                if (isSuperior)
                {
                    if (player.currentCoin >= coinCondition)
                    {
                        player.currentCoin += CoinUpdate;
                        player.knowledgePoint += knowledgeUpdate;
                    }
                } else
                {
                    if (player.currentCoin <= coinCondition)
                    {
                        player.currentCoin += CoinUpdate;
                        player.knowledgePoint += knowledgeUpdate;
                    }
                }
            }
        }
        else if (isConditionKnowledge)
        {
            foreach (PlayerController player in PlayerManager.Instance.allPlayer)
            {
                if (isSuperior)
                {
                    if (player.knowledgePoint >= knowledgeCondition)
                    {
                        player.currentCoin += CoinUpdate;
                        player.knowledgePoint += knowledgeUpdate;
                    }
                }
                else
                {
                    if (player.knowledgePoint <= knowledgeCondition)
                    {
                        player.currentCoin += CoinUpdate;
                        player.knowledgePoint += knowledgeUpdate;
                    }
                }
            }
        }
    }
}
