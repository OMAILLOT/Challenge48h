using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField] GameObject startTurnButton;
    [SerializeField] GameObject nextTurnButton;
    [SerializeField] TextMeshProUGUI randomNumberText;
    [SerializeField] TextMeshProUGUI playerWhoPlayText;

    public int randomNumber;
    public void StartTurnUI()
    {
        playerWhoPlayText.text = PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].playerName;
        startTurnButton.SetActive(true);
        nextTurnButton.SetActive(false);
        randomNumberText.text = "";
    }
    public void ButtonStartPress()
    {
        playerWhoPlayText.text = "";
        PlayerManager.Instance.MooveCurrentPlayer();
        randomNumberText.text = randomNumber.ToString();
        startTurnButton.SetActive(false);
        nextTurnButton.SetActive(true);
    }

    public void nextTurnbuttonPress()
    {
        PlayerManager.Instance.NextTurn();
    }


}
