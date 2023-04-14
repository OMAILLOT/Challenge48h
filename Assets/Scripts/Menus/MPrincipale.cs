using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPrincipale : MonoBehaviour
{

    [SerializeField] private CanvasGroup principalCanva;
    [SerializeField] private CanvasGroup optionCanva;

    public void PlayButton()
    {
        Debug.Log("Play !");
    }

    public void OptionsButton()
    {
        principalCanva.alpha = 0;
        principalCanva.interactable = false;
        principalCanva.blocksRaycasts = false;
        optionCanva.alpha = 1;
        optionCanva.interactable = true;
        optionCanva.blocksRaycasts = true;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
}
