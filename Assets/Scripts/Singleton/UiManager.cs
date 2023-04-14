using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField] CanvasGroup GameView;
    [SerializeField] CanvasGroup EndView;
    [Space(10)]
    [SerializeField] GameObject startTurnButton;
    [SerializeField] GameObject nextTurnButton;
    [Space(10)]
    [SerializeField] TextMeshProUGUI randomNumberText;
    [SerializeField] TextMeshProUGUI playerWhoPlayText;
    [SerializeField] TextMeshProUGUI topPlayer;
    [SerializeField] TextMeshProUGUI otherPlayer;

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
            randomNumberText.text = Random.Range(PlayerManager.Instance.minDiceNumber, PlayerManager.Instance.maxDiceNumber).ToString();
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



    public void RestartGameButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
