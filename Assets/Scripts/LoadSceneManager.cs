using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{

    public AudioClip onClick;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadMain()
    {
        audioSource.PlayOneShot(onClick, 0.7F);
        SceneManager.LoadScene("ConversationScene", LoadSceneMode.Single);
    }

    public void LoadCredits()
    {
        audioSource.PlayOneShot(onClick, 0.7F);
        SceneManager.LoadScene("CreditScreen", LoadSceneMode.Single);
    }

    public void Exit()
    {
        //Application.Quit();
    }
}
