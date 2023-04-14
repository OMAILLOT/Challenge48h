using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BaseTemplate.Behaviours;

public class QuestionManager : MonoSingleton<QuestionManager>
{
    public CardQuestion questionCard;
    
    public CardQuestion PeekCard(bool isEasy, bool isInteraction)
    {
        string easyOrHard = isEasy ? "easy" : "hard";
        string filePath = Application.dataPath + "/QuestionsFiles/questions_" + easyOrHard + "QI.csv";
        string filePathInter = Application.dataPath + "/QuestionsFiles/questions_interactions.csv";

        StreamReader reader = null;
        if (isInteraction)
        {
            reader = new StreamReader(filePathInter);
        }
        else
        {
            reader = new StreamReader(filePath);
        }

        List<CardQuestion> questionCards = new List<CardQuestion>();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(';');

            CardQuestion card = ScriptableObject.CreateInstance<CardQuestion>();
            card.question = values[0];

            List<string> answers = new List<string>();

            for (int i = 1; i < values.Length; i++)
            {
                answers.Add(values[i]);
            }

            card.reponse = answers;
            card.bonneReponse = answers[1];
            questionCards.Add(card);
        }

        CardQuestion randomQuestion = questionCards[Random.Range(0, questionCards.Count)];

        Debug.Log("Question : " + randomQuestion.question);
        foreach (string answer in randomQuestion.reponse)
        {
            Debug.Log("Réponse : " + answer);
        }

        reader.Close();

        return randomQuestion;
    }
}