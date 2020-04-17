using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAdRewardInGame : MonoBehaviour {

	public GameObject doubledStars;
	public Text doubledTxt;
	public GameObject adButton;

	GameManager gameManager;

	void Awake()
	{
		Advertisement.Initialize("1676954");
	}

	void Start()
	{
		
		doubledStars.SetActive (false);
		gameManager = FindObjectOfType<GameManager> ();
	}

	public void ShowAdRevive()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResultRevive });
		}
	}

	public void ShowAdStars()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResultStars });
		}
	}

	private void HandleAdResultRevive(ShowResult result)
	{
		switch (result) {
		case ShowResult.Finished:
			//revive
			gameManager.Revive ();
			break;
		case ShowResult.Skipped:
			Debug.Log ("skipped");
			break;
		case ShowResult.Failed:
		//restart if failed
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
			Debug.Log ("Failed to load");
			break;
		}

	}

	private void HandleAdResultStars(ShowResult result)
	{
		switch (result) {
		case ShowResult.Finished:
			adButton.GetComponent<Button>().interactable = false;
			doubledStars.SetActive (true);
			doubledTxt.text = "After watching the ad you have doubled your stars. For this run you earned " + gameManager.stars * 2 + " stars! \n Congratulations! No go deliver more!";
			PlayerPrefs.SetInt("Stars", gameManager.stars + PlayerPrefs.GetInt("Stars"));
			break;
		case ShowResult.Skipped:
			Debug.Log ("skipped");
			break;
		case ShowResult.Failed:
			Debug.Log ("Failed to load");
			break;
		}
	}
}
