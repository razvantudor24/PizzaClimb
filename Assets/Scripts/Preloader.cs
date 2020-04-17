using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float loadTime;
	private float minLogoTime = 3.0f;

	// Use this for initialization
	void Start () {

		fadeGroup = FindObjectOfType<CanvasGroup> ();

		fadeGroup.alpha = 1;

		if (Time.time < minLogoTime)
			loadTime = minLogoTime;
		else
			loadTime = Time.time;
	}
	
	// Update is called once per frame
	private void Update () {

		if (Time.time < minLogoTime)
			fadeGroup.alpha = 1 - Time.time;

		if (Time.time > minLogoTime && loadTime != 0) {
			fadeGroup.alpha = Time.time - minLogoTime;
			if (fadeGroup.alpha >= 1)
				SceneManager.LoadScene ("Menu");
		}
	}
}
