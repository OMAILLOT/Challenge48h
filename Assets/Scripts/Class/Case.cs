using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    
    public TypeCase type;
    [SerializeField] List<Transform> placeHolderPlayer;
    public List<PlayerController> playerOnCase = new List<PlayerController>();

    public virtual void PlayerOnthisCase(PlayerController currentPlayer, bool isPlayAnimationForThisPlayer = true)
    {
        playerOnCase.Add(currentPlayer);

        currentPlayer.OnPlayerEnter(this);
        if (playerOnCase.Count > 1)
        {
            for (int i = 0; i < playerOnCase.Count; i++)
            {
                if (playerOnCase[i] == currentPlayer && !isPlayAnimationForThisPlayer)
                {

                } else
                {
                    playerOnCase[i].transform.DOMove(placeHolderPlayer[i].position + Vector3.up * 1,.5f);
                }
            }
        }
    }

    public virtual void ResetPlayerOnCase(PlayerController currentPlayer)
    {
        playerOnCase.Remove(currentPlayer);

        if (playerOnCase.Count > 1)
        {
            for (int i = 0; i < playerOnCase.Count; i++)
            {
                playerOnCase[i].transform.DOMove(placeHolderPlayer[i].position + Vector3.up * 1, .5f);
            }
        } else if (playerOnCase.Count == 1) 
        {
            playerOnCase[0].transform.DOMove(transform.position + Vector3.up * 1, .5f);
        }
    } 
}
