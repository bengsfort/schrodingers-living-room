﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
	public void LoadIntro() {
		SceneManager.LoadScene("IntroScreen", LoadSceneMode.Single);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.anyKey) {
			LoadIntro ();
		}
    }
}
