using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    
    public TypeCase type;
    [SerializeField] List<Transform> placeHolderPlayer;
    public List<PlayerController> playerOnCase = new List<PlayerController>();

    public virtual void OnStartCase(PlayerController currentPlayer)
    {
        playerOnCase.Add(currentPlayer);

        currentPlayer.OnPlayerEnter(this);
        if (playerOnCase.Count > 1)
        {
            for (int i = 0; i < playerOnCase.Count; i++)
            {
                playerOnCase[i].transform.position = placeHolderPlayer[i].position + Vector3.up * 1;
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
                playerOnCase[i].transform.position = placeHolderPlayer[i].position + Vector3.up * 1;
            }
        } else if (playerOnCase.Count == 1) 
        {
            playerOnCase[0].transform.position = transform.position + Vector3.up * 1;
        }
    } 
}
