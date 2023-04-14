using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPrincipale : MonoBehaviour
{

    [SerializeField] private CanvasGroup principalCanva;
    [SerializeField] private CanvasGroup optionCanva;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipOnClick;

    public void PlayButton()
    {
        audioSource.PlayOneShot(audioClipOnClick);
        SceneManager.LoadScene("MainScene");
    }

    public void OptionsButton()
    {
        audioSource.PlayOneShot(audioClipOnClick);
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
