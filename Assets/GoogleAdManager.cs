using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class GoogleAdManager : MonoBehaviour {

	public string bannerID;
	BannerView banner;
	InterstitialAd interstitialAd;

	// Use this for initialization
	void Start () {
		RequestBanner ();
		ShowBanner ();
	}

	void RequestBanner()
	{
		banner = new BannerView (bannerID, AdSize.SmartBanner, AdPosition.Bottom);
		AdRequest adRequest = new AdRequest.Builder ().Build ();
		banner.LoadAd (adRequest);
	}

	public void ShowBanner()
	{
		
		banner.Show();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
