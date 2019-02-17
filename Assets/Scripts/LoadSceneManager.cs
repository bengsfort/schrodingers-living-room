using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
	public void LoadMain() {
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}

	public void LoadCredits() {
		SceneManager.LoadScene("CreditScreen", LoadSceneMode.Single);
	}

	public void Exit() {
		//Application.Quit();
    }
}
