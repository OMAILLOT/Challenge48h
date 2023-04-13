using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TextMeshPro questionText;
    public List<Question> questionsList;
    public int qipointBonus = 10; // Nombre de points QI à gagner pour une question QI bonus
    public int argentBonus = 50; // Montant d'argent à gagner pour une question argent bonus
    public int argentMalus = -25; // Montant d'argent à perdre pour une question argent malus
    public int score = 0; // Score du joueur (QI ou argent, selon le type de question)

    void Start()
    {
        // Ajouter ici toutes les questions à afficher dans la liste "questionsList"
        questionsList = new List<Question>(); // Initialise la liste avant de la remplir
        questionsList.Add(new Question("Quel est le nom de la capitale de la France ?", "Paris", "qi"));
        questionsList.Add(new Question("Combien de planètes y a-t-il dans le système solaire ?", "8", "qi"));
        questionsList.Add(new Question("Quel est l'inventeur de la théorie de la relativité ?", "Albert Einstein", "qi"));
        questionsList.Add(new Question("Quel est le nom du président français actuel ?", "Emmanuel Macron", "argentBonus"));
        questionsList.Add(new Question("Quelle est la plus haute montagne du monde ?", "Everest", "argentMalus"));
        questionsList.Add(new Question("En quelle année est née Marie Curie ?", "1867", "qi"));

        // Récupérer ici la référence de la case sur laquelle le joueur est tombé
        GameObject caseJoueur = GameObject.FindGameObjectWithTag("CaseJoueur");

        // Vérifier si c'est une case "bonus" ou "malus"
        if (caseJoueur.GetComponent<Case>().type == TypeCase.Chance)
        {
            // Choisir aléatoirement une question dans la liste "questionsList"
            Question randomQuestion = questionsList[Random.Range(0, questionsList.Count)];
            questionText.text = randomQuestion.question;

            // Attendre la réponse du joueur
            StartCoroutine(WaitForAnswer(randomQuestion));
        }
/*        else if (caseJoueur.GetComponent<Case>().type == TypeCase.Malus)
        {
            // Choisir une autre question dans la liste "questionsList"
            Question randomQuestion = questionsList[Random.Range(0, questionsList.Count)];
            questionText.text = randomQuestion.question;

            // Attendre la réponse du joueur
            StartCoroutine(WaitForAnswer(randomQuestion));
        }*/
    }

    IEnumerator WaitForAnswer(Question question)
    {
        // Attendre la réponse du joueur
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            // Vérifier si le joueur a appuyé sur le bouton de validation de réponse
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Vérifier si la réponse est correcte
                if (question.CheckAnswer())
                {
                    // Attribuer des points ou de l'argent en fonction du type de question
                    if (question.type == "qi")
                    {
                        score += qipointBonus;
                    }
                    else if (question.type == "argentBonus")
                    {
                        score += argentBonus;
                    }
                    else if (question.type == "argentMalus")
                    {
                        score += argentMalus;
                    }

                    Debug.Log("Bonne réponse");
                    // Mettre à jour le score du joueur
                    GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = "Score : " + score;

                    // Quitter la boucle de vérification de réponse
                    break;
                }
                else
                {
                    // Si la réponse est incorrecte, ne rien faire et attendre que le joueur essaye à nouveau
                    Debug.Log("Mauvaise réponse");
                }
            }
        }
    }
}

public class Question
{
    public string question;
    public string answer;
    public string type;

    public Question(string q, string a, string t)
    {
        question = q;
        answer = a;
        type = t;
    }

    public bool CheckAnswer(string answer=null)
    {
        // Instancier l'objet InputField
        GameObject inputFieldObject = GameObject.Find("InputField");

        // Vérifier si la réponse est correcte
        if (inputFieldObject != null)
        {
            InputField inputField = inputFieldObject.GetComponent<InputField>();
            if (inputField != null)
            {
                return (inputField.text == answer);
            }
        }

        return false;
    }
}