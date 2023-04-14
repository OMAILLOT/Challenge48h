using BaseTemplate.Behaviours;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardManager : MonoSingleton<CardManager>
{

    //public List<CardList> allCards;
    public List<ChanceCard> chanceCards;
    public List<CardQuestion> cardQuestions;
    public List<EvenmentCard> evenmentCards;
    public List<InteractionCard> interactionCard;

    public void ActiveCard(TypeCase typeCase)
    {
        switch (typeCase)
        {
            case TypeCase.Evenement:
                UiManager.Instance.ActiveEvenmentCardUi(evenmentCards[Random.Range(0, evenmentCards.Count)]);
                break;
            case TypeCase.Chance:
                UiManager.Instance.ActiveChanceCard(chanceCards[Random.Range(0, chanceCards.Count)]);
                break;
            case TypeCase.qiPoints:
                UiManager.Instance.ActivePreQuestionCardUi(QuestionManager.Instance.PeekCard(true,false));
                break;
            case TypeCase.Interaction:
                UiManager.Instance.ActiveInteractionCard(interactionCard[Random.Range(0, interactionCard.Count)]);
                break;
        }
                
    }
}

/*[Serializable]
public class CardList
{
    public TypeCase typeCase;
    public List<ScriptableObject> allCards;
}*/
