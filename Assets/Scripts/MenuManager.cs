using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float fadeInSpeed = 0.33f;


	//UI
	public GameObject howToP;
	public GameObject creditsP;
	public GameObject carSelect;
	public GameObject[] buttonsToDisable;
	public Text coinsTxt;
	public Text starsTxt;

	public AudioClip menuTap;


	//COINS & STARS
	public int userCoins;
	public int userStars;

	// Use this for initialization
	void Start () {
		carSelect.SetActive (false);
		howToP.SetActive (false);
		creditsP.SetActive (false);

		//fadeGroup = FindObjectOfType<CanvasGroup> ();
		//fadeGroup.alpha = 1;

		userCoins = PlayerPrefs.GetInt ("Coins");
		coinsTxt.text = userCoins.ToString();
		userStars = PlayerPrefs.GetInt ("Stars");
		starsTxt.text = userStars.ToString();
	}

	public void Play()
	{	
		playSfx (menuTap);
		carSelect.SetActive (true);
		foreach (GameObject buton in buttonsToDisable) 
		{
			buton.GetComponent<Button> ().interactable = false;
		}
	}

	public void DisplayCredits()
	{
		playSfx (menuTap);
		creditsP.SetActive (true);
		foreach (GameObject buton in buttonsToDisable) 
		{
			buton.GetComponent<Button> ().interactable = false;
		}
	}

	public void DisplayInstr()
	{
		playSfx (menuTap);
		howToP.SetActive (true);
		foreach (GameObject buton in buttonsToDisable) 
		{
			buton.GetComponent<Button> ().interactable = false;
		}
	}

	public void Close(GameObject panel)
	{
		playSfx (menuTap);
		panel.SetActive (false);
		foreach (GameObject buton in buttonsToDisable) 
		{
			buton.GetComponent<Button> ().interactable = true;
		}
	}

	public void ShowAd()
	{
		playSfx (menuTap);
		Debug.Log ("Show Ad");
	}

	public void Twitter()
	{
		playSfx (menuTap);
		Application.OpenURL ("https://twitter.com/TripleRStudios");
	}

	public void StartLevel(string levelName)
	{
		playSfx (menuTap);
		SceneManager.LoadScene (levelName);
	}

	void playSfx(AudioClip _sfx) {
		GetComponent<AudioSource>().clip = _sfx;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		userCoins = PlayerPrefs.GetInt ("Coins");
		coinsTxt.text = userCoins.ToString();
		userStars = PlayerPrefs.GetInt ("Stars");
		starsTxt.text = userStars.ToString();
		//fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

	}
}
