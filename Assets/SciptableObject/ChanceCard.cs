using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Card", menuName = "Card/ChanceCard", order = 1)]
public class ChanceCard : ScriptableObject
{
    public string Description;
    public int CoinUpdate;
    public int knowledgeUpdate;

    public void ActiveChanceCard()
    {
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].currentCoin += CoinUpdate;
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint += knowledgeUpdate;
    }
}
