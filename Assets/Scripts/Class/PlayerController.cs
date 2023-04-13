using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int currentCoin;
    public float average;
    public bool isMyTurn = false;

    public void StartMyTurn()
    {
        isMyTurn = true;
    }
}
