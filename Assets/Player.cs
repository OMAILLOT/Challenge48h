using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money;
    public int qiPoints;

   public void OnPlayerEnter(Case currentCase)
{
    if (currentCase.type == Case.TypeCase.Bonus)
    {
        // Si la case est de type "Bonus", ajouter de l'argent au joueur
        money += 100;
        Debug.Log("Vous avez gagné 100 € !");
    }
    else if (currentCase.type == Case.TypeCase.Malus)
    {
        // Si la case est de type "Malus", retirer de l'argent au joueur
        money -= 50;
        Debug.Log("Vous avez perdu 50 € !");
    }   
    else if (currentCase.type == Case.TypeCase.qiPoints)
    {
        // Si la case est de type "QI", retirer du QI au joueur
        qiPoints -= 50;
        Debug.Log("Vous avez perdu 50 points de QI !");
    }
    else
    {
        // Si la case est de type "Normal", ne rien faire
        Debug.Log("Case normale !");
    }
}
}