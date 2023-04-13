using BaseTemplate.Behaviours;
using System.Collections;
using TMPro;
using UnityEngine;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField] GameObject startTurnButton;
    [SerializeField] GameObject nextTurnButton;
    [SerializeField] TextMeshProUGUI randomNumberText;
    [SerializeField] TextMeshProUGUI playerWhoPlayText;

    [SerializeField] float timeToChangeNumber;

    public int randomNumber;
    public void StartTurnUI()
    {
        playerWhoPlayText.text = PlayerManager.Instance.allPlayer[PlayerManager.Instance.currentIndexPlayer].playerName;
        startTurnButton.SetActive(true);
        nextTurnButton.SetActive(false);
        randomNumberText.text = "";
        StartCoroutine(DiceLoop());

    }
    public void ButtonStartPress()
    {
        StopAllCoroutines();
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

    IEnumerator DiceLoop()
    {
        while (true)
        {
            randomNumberText.text = Random.Range(2, 12).ToString();
            yield return new WaitForSeconds(timeToChangeNumber);
        }
    }

}
