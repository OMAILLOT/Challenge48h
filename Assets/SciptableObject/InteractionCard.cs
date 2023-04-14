using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionCard", menuName = "Card/InteractionCard", order = 1)]
public class InteractionCard : ScriptableObject
{
    public string desctiption;
    public int CoinUpdate;
    public int knowledgeUpdate;

    public void ActiveInteractionCard()
    {
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].currentCoin += CoinUpdate;
        PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint += knowledgeUpdate;
    }
}
