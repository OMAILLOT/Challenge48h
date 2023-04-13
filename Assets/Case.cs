using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    public enum TypeCase { Normal, Bonus, Malus, qiPoints };
    public TypeCase type;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Si le joueur entre en collision avec la case, appeler la fonction "OnPlayerEnter"
            other.GetComponent<Player>().OnPlayerEnter(this);
        }
    }
}