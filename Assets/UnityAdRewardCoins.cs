using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAdRewardCoins : MonoBehaviour {

	public int bonusCoins;
	public int bonusStars;

	void Awake()
	{
		Advertisement.Initialize("1676954");
	}

	public void ShowAd()
	{
		if (Advertisement.IsReady ()) {
			
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResult });
		}
		
	}

	public void ShowAdStars()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResultStars });
		}
	}

	private void HandleAdResult(ShowResult result)
	{
		switch (result) {
		case ShowResult.Finished:
			//reward user with coins;
			PlayerPrefs.SetInt("Coins", bonusCoins + PlayerPrefs.GetInt("Coins"));
			break;
		case ShowResult.Skipped:
			Debug.Log ("skipped");
			break;
		case ShowResult.Failed:
			Debug.Log ("Failed to load");
			break;
		}

	}

	private void HandleAdResultStars(ShowResult result)
	{
		switch (result) {
		case ShowResult.Finished:
			//reward user with coins;
			PlayerPrefs.SetInt("Stars", bonusStars + PlayerPrefs.GetInt("Stars"));
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
