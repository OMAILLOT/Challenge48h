using BaseTemplate.Behaviours;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{
    public CanvasGroup evenementPanel;
    public CanvasGroup iQPanel;
    public CanvasGroup chancePanel;
    public CanvasGroup interactionPanel;
    public CanvasGroup preQuestionPanel;


    [Space(5)]
    [SerializeField] CanvasGroup GameView;
    [SerializeField] CanvasGroup EndView;
    [Space(10)]
    [SerializeField] Button startTurnButton;
    //[SerializeField] Button nextTurnButton;
    [Space(10)]
    [SerializeField] TextMeshProUGUI randomNumberText;
    [SerializeField] TextMeshProUGUI topPlayer;
    [SerializeField] TextMeshProUGUI otherPlayer;

    [SerializeField] float timeToChangeNumber;

    [Header("Question Panel")]
    [SerializeField] TextMeshProUGUI questionDescription;
    [SerializeField] List<TextMeshProUGUI> allReponses;

    [Header("Chance Panel")]
    [SerializeField] TextMeshProUGUI chanceDescription;

    [Header("Evenement panel")]
    [SerializeField] TextMeshProUGUI evenmenentDescription;
    
    [Header("Interactive panel")]
    [SerializeField] TextMeshProUGUI interactiveDescription;
    [SerializeField] TextMeshProUGUI leftButton;
    [SerializeField] TextMeshProUGUI rightButton;


    EvenmentCard currentEvenmentCard;
    CardQuestion currentCardQuestion;
    InteractionCard currentInteractionCard;
    ChanceCard currentChanceCard;
    bool isCurrentCardEasy;

    TypeCase currentTypeCase;


    public int randomNumber;
    public void StartTurnUI()
    {
        startTurnButton.interactable = true;
        randomNumberText.text = "";
        StartCoroutine(DiceLoop());

    }
    public void ButtonStartPress()
    {
        StopAllCoroutines();
        //playerWhoPlayText.text = "";
        PlayerManager.Instance.MooveCurrentPlayer();
        randomNumberText.text = randomNumber.ToString();
        startTurnButton.interactable = false;
    }

    public void nextTurnbuttonPress()
    {
        PlayerManager.Instance.NextTurn();
    }

    IEnumerator DiceLoop()
    {
        while (true)
        {
            randomNumberText.text = UnityEngine.Random.Range(PlayerManager.Instance.minDiceNumber, PlayerManager.Instance.maxDiceNumber).ToString();
            yield return new WaitForSeconds(timeToChangeNumber);
        }
    }

    public void EndGamePanel()
    {
        GameView.interactable = false;
        GameView.blocksRaycasts = false;
        topPlayer.text = "";
        otherPlayer.text = "";

        for (int i = 0; i < PlayerManager.Instance.numberOfWinner; i++)
        {
            topPlayer.text += $"{PlayerManager.Instance.bestPlayerInEndGame[i].playerName}\n";
        }
        for (int i = PlayerManager.Instance.numberOfWinner; i < PlayerManager.Instance.bestPlayerInEndGame.Count; i++)
        {
            otherPlayer.text += $"{PlayerManager.Instance.bestPlayerInEndGame[i].playerName},\n";
        }
        DOVirtual.Float(GameView.alpha, 0, .5f, a => GameView.alpha = a).SetDelay(1f);
        DOVirtual.Float(EndView.alpha, 1, .5f, a => EndView.alpha = a).SetDelay(1f).OnComplete(() => {
            EndView.interactable = true;
            EndView.blocksRaycasts = true;
        });
    }

    #region evenment
    public void ActiveEvenmentCardUi(EvenmentCard evenmentCard)
    {
        evenmenentDescription.text = evenmentCard.Description;
        evenementPanel.alpha = 1;
        evenementPanel.interactable = true;
        evenementPanel.blocksRaycasts = true;
        currentEvenmentCard = evenmentCard;
    }

    public void ActiveEvent()
    {
        evenementPanel.alpha = 0f;
        evenementPanel.interactable = false;
        evenementPanel.blocksRaycasts = false;
        currentEvenmentCard.ActiveEvenmentCard();
        nextTurnbuttonPress();
    }
    #endregion

    #region QuestionCard

    public void ActivePreQuestionCardUi(CardQuestion cardQuestion)
    {
        preQuestionPanel.alpha = 1f;
        preQuestionPanel.interactable = true;
        preQuestionPanel.blocksRaycasts = true;
        currentCardQuestion = cardQuestion;
    }
    public void ActiveQuestionCardUi()
    {
        preQuestionPanel.alpha = 0;
        iQPanel.interactable = false;
        iQPanel.blocksRaycasts = false;

        questionDescription.text = currentCardQuestion.question;

        for (int i = 0; i < currentCardQuestion.reponse.Count; i++)
        {
            allReponses[i].text = currentCardQuestion.reponse[i];
        } 

        iQPanel.alpha = 1;
        iQPanel.interactable = true;
        iQPanel.blocksRaycasts = true;
        
    }

    public void CheckQuestion(int index)
    {
        if (currentCardQuestion.bonneReponse == currentCardQuestion.reponse[index])
        {
            if (isCurrentCardEasy) PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint += 100;
            else PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint += 200;
        } else
        {
            if (isCurrentCardEasy) PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint -= 100;
            else PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].knowledgePoint -= 200;
        }
        iQPanel.alpha = 0f;
        iQPanel.interactable = false;
        iQPanel.blocksRaycasts = false;
        nextTurnbuttonPress();
    }

    public void CheckLevelQuetion(bool isEasy)
    {
        preQuestionPanel.alpha = 0f;
        preQuestionPanel.interactable = false;
        preQuestionPanel.blocksRaycasts = false;
        isCurrentCardEasy = isEasy;

        QuestionManager.Instance.PeekCard(isEasy,false);

        ActiveQuestionCardUi();
    }
    #endregion

    #region InteractionCard

    public void ActiveInteractionCard(InteractionCard interactionCard)
    {
        interactiveDescription.text = interactionCard.desctiption;
        leftButton.text = interactionCard.firstButtonText;
        rightButton.text = interactionCard.secondButtonText;

        currentInteractionCard = interactionCard;
        interactionPanel.alpha = 1f;
        interactionPanel.interactable = true;
        interactionPanel.blocksRaycasts = true;
    }

    public void ActiveInteraction(bool isAccept)
    {
        if (isAccept)
        {
            currentInteractionCard.ActiveInteractionCard();
        }
        interactionPanel.alpha = 0f;
        interactionPanel.interactable = false;
        interactionPanel.blocksRaycasts = false;
        nextTurnbuttonPress();
    }

    #endregion

    #region ChanceCard 
    public void ActiveChanceCard(ChanceCard chanceCard)
    {
        chanceDescription.text = chanceCard.Description;

        currentChanceCard = chanceCard;
        chancePanel.alpha = 1f;
        chancePanel.interactable = true;
        chancePanel.blocksRaycasts = true;
    }

    public void ActiveChance()
    {
        chancePanel.alpha = 0f;
        chancePanel.interactable = false;
        chancePanel.blocksRaycasts = false;
        currentChanceCard.ActiveChanceCard();
        nextTurnbuttonPress();
    }
    #endregion

    public void RestartGameButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

[Serializable]
public class AllPanelCard
{
    public TypeCase typePanel;
    public CanvasGroup canvasGroup;
}
