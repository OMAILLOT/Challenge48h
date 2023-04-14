using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestionCard", menuName = "Card/QuestionCard", order = 1)]
public class CardQuestion : ScriptableObject
{
   public string question;
   public List<string> reponse;
   public string bonneReponse;
   public List<QuestionData> questions;

}
