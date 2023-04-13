using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeCase { Normal, Chance, qiPoints, Evenement};
public class BoardManager : MonoSingleton<BoardManager>
{
    public List<Case> allCases;

}
