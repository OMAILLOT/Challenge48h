using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public List<PlayerController> allPlayer;
    public List<GameObject> playeryStatsTurnUi;
    public List<Transform> playerStartAndEnd;
    public int minDiceNumber;
    public int maxDiceNumber;
    public int currentIndexPlayer;
    [SerializeField] int startPlayerCoin;
    [SerializeField] int startAveragePoint;
    public int numberOfTurn;

    List<string> playerNameForCamera = new List<string>();
    public List<PlayerController> bestPlayerInEndGame = new List<PlayerController>();
    public int numberOfWinner = 1;

    public void Start()
    {
        for (int i = 0; i < allPlayer.Count; i++) 
        {
            bestPlayerInEndGame.Add(allPlayer[i]);
            playerNameForCamera.Add($"Player{i+1}");
            allPlayer[i].transform.position = playerStartAndEnd[i].position;
            allPlayer[i].currentCoin = startPlayerCoin;
            allPlayer[i].knowledgePoint = startAveragePoint;
        }
        StartFirstTurn();
    }

    public void StartFirstTurn()
    {
        currentIndexPlayer = Random.Range(0, allPlayer.Count);
        allPlayer[currentIndexPlayer].StartMyTurn();
        UiManager.Instance.StartTurnUI();
        CameraManager.Instance.CameraSwitch(playerNameForCamera[currentIndexPlayer]);
        playeryStatsTurnUi[currentIndexPlayer].SetActive(true);

    }
    public void NextTurn()
    {
        allPlayer[currentIndexPlayer].isMyTurn = false;
        playeryStatsTurnUi[currentIndexPlayer].SetActive(false);
        currentIndexPlayer++;
        if (currentIndexPlayer >= allPlayer.Count) currentIndexPlayer = 0;

        playeryStatsTurnUi[currentIndexPlayer].SetActive(true);

        CameraManager.Instance.CameraSwitch(playerNameForCamera[currentIndexPlayer]);
        allPlayer[currentIndexPlayer].StartMyTurn();
        UiManager.Instance.StartTurnUI();
    }

    public void EndGame()
    {
        numberOfWinner = 1;
        foreach (PlayerController player in bestPlayerInEndGame) 
        {
            // player.finalPoint = calcule
            player.totalScore = (int) (player.knowledgePoint * 100) + (player.currentCoin / 80 * 100);
        }
        bestPlayerInEndGame.Sort((p1, p2) => p1.totalScore.CompareTo(p2.totalScore));
        bestPlayerInEndGame.Reverse();
        for ( int i = 0; i < bestPlayerInEndGame.Count; i++ )
        {
            if (i < bestPlayerInEndGame.Count-2 && bestPlayerInEndGame[i].totalScore == bestPlayerInEndGame[i + 1].totalScore)
            {
                numberOfWinner++;
            }
            else
            {
                if (i == bestPlayerInEndGame.Count - 1) numberOfWinner++;
                break;
            }
        }
        UiManager.Instance.EndGamePanel();
    }

    public void MooveCurrentPlayer()
    {
        foreach (PlayerController player in allPlayer)
        {
            if (player.isMyTurn)
            {
                player.PlayerMoove();
            }
        }
    }

    public void PlayerFinish()
    {
        int index = currentIndexPlayer;
        allPlayer[index].isMyTurn = false;
        allPlayer[index].transform.position = playerStartAndEnd[index].position;
        playerStartAndEnd.RemoveAt(index);
        allPlayer.RemoveAt(index);
        playerNameForCamera.RemoveAt(index);
        currentIndexPlayer++;
        if (currentIndexPlayer >= allPlayer.Count) currentIndexPlayer = 0;
        if (allPlayer.Count <= 0)
        {
            EndGame();
        } else
        {
            StartCoroutine(WaitBeforeChangePlayer());
        }
        
    }

    IEnumerator WaitBeforeChangePlayer()
    {
        yield return new WaitForSeconds(2f);
        UiManager.Instance.nextTurnbuttonPress();
    }
}
