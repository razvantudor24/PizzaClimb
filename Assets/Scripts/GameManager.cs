using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//UI stuff
	[Header("UI")]
	public Text timerTxt;
	public GameObject pauseMenu;
	public GameObject gameOverDie;
	public GameObject gameOverWin;
	public GameObject gameplayCanvas;
	public Text winText;
	public Text coinsText;

	public GameObject splashScreen;

	public GameObject item1Image;
	public GameObject item2Image;
	public GameObject item3Image;
	public GameObject bonusTxt;

	[Header("Audio")]
	public AudioClip menuTap;
	public AudioClip gameWon;
	public AudioClip gameLost;

	public GameObject audioMngr;

	//stars
	[Header("Stars")]
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	public GameObject noStars;
	//mechanics
	private float startTime;
	private float t;
	private bool finish = false;
	public int stars;
	Coin coinManager;
	EndConditions conditionManager;
	public bool isPaused;
	//timers for stars
	public int oneStarTime;
	public int twoStarTime;
	public int threeStarTime;


	Vector3 playerPositon;


	// Use this for initialization
	void Start () {

		isPaused = false;
		Time.timeScale = 1;
		startTime = Time.time;
		gameplayCanvas.SetActive (true);
		pauseMenu.SetActive (false);
		gameOverDie.SetActive (false);
		gameOverWin.SetActive (false);
		star1.SetActive(false);
		star2.SetActive (false);
		star3.SetActive (false);
		noStars.SetActive (false);

		item1Image.SetActive (false);
		item2Image.SetActive (false);
		item3Image.SetActive (false);
		bonusTxt.SetActive (false);

		coinsText.text = PlayerPrefs.GetInt ("Coins").ToString();
		coinManager = FindObjectOfType<Coin> ();
		conditionManager = FindObjectOfType<EndConditions> ();

	
	}
	
	// Update is called once per frame
	void Update () {
		if (finish)
			return;

		t = Time.time - startTime;

		string minutes = ((int) t/60).ToString();
		string seconds = (t % 60).ToString ("f2");

		timerTxt.text = minutes + "." + seconds;

		if (t <= threeStarTime) {
			timerTxt.color = Color.green;
		} else if (t > threeStarTime && t <= twoStarTime) {
			timerTxt.color = Color.yellow;
		} else if (t > twoStarTime && t <= oneStarTime) {
			timerTxt.color = Color.blue;
		} else {
			timerTxt.color = Color.red;
		}

		//give bonus coins
		if (conditionManager.itemCount >=3) {
			bonusTxt.SetActive (true);
			//play bonus soud
		}

		//cancel fade
		if (splashScreen.GetComponent<Image> ().color.a == 0)
			splashScreen.SetActive (false);
	
	}

	public void Revive()
	{
		gameplayCanvas.SetActive (true);
		gameOverDie.SetActive (false);
		Time.timeScale = 1;
		finish = false;
		conditionManager.transform.localEulerAngles = new Vector3 (0, 0, 0); 
		conditionManager.GetComponent<AudioSource> ().Play ();
	}

	public void GameOverDie()
	{
		playSfx (gameLost);
		gameOverDie.SetActive (true);
		gameplayCanvas.SetActive (false);
		Time.timeScale = 0;
		finish = true;
		timerTxt.color = Color.yellow;
		splashScreen.GetComponent<Image> ().enabled = false;
	}

	public void GameOverWin()
	{
		gameOverWin.SetActive (true);
		gameplayCanvas.SetActive (false);
		timerTxt.enabled = false;
		//PlayerPrefs.SetFloat("BestTime", t); 
		playSfx(gameWon);
		Time.timeScale = 0;
		winText.text = "Your pizza was delivered in " + t.ToString("f2") + " seconds. Improve your time or head to the next level! Good Luck!";


		//Show Stars
		if (t <= threeStarTime) {
			star1.SetActive (true);
			star2.SetActive (true);
			star3.SetActive (true);
			timerTxt.color = Color.green;
			stars = 3;
			//SET player prefs stars for each level
		} else if (t > threeStarTime && t <= twoStarTime) {
			star1.SetActive (true);
			star2.SetActive (true);
			star3.SetActive (false);
			timerTxt.color = Color.yellow;
			stars = 2;
			//SET player prefs stars for each level
		} else if (t > twoStarTime && t <= oneStarTime) {
			star1.SetActive (true);
			star2.SetActive (false);
			star3.SetActive (false);
			timerTxt.color = Color.blue;
			stars = 1;
			//SET player prefs stars for each level
		} else {
			star1.SetActive (false);
			star2.SetActive (false);
			star3.SetActive (false);
			noStars.SetActive (true);
			timerTxt.color = Color.red;
			stars = 0;
		}

		//set total stars
		PlayerPrefs.SetInt("Stars", stars + PlayerPrefs.GetInt("Stars"));

	}

	public void PickUpCoin()
	{
		//PlayerPrefs.SetInt ("Coins", coinManager.coinValue + PlayerPrefs.GetInt ("Coins"));
		coinsText.text =  PlayerPrefs.GetInt ("Coins").ToString();

	}

	public void PauseGame()
	{
		isPaused = true;
		playSfx (menuTap);
		Time.timeScale = 0;
		pauseMenu.SetActive (true);
		splashScreen.GetComponent<Image> ().enabled = false;
		gameplayCanvas.SetActive (false);
	}

	public void ResumeGame()
	{
		isPaused = false;
		playSfx (menuTap);
		Time.timeScale = 1;
		pauseMenu.SetActive (false);
		gameplayCanvas.SetActive (true);
	}
	public void RestartGame()
	{
		playSfx (menuTap);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
	public void BackToMenu()
	{
		playSfx (menuTap);
		SceneManager.LoadScene ("Menu");
		//Debug.Log ("back to menu");
	}

	void playSfx(AudioClip _sfx) {
		audioMngr.GetComponent<AudioSource>().clip = _sfx;
		if(!audioMngr.GetComponent<AudioSource>().isPlaying)
			audioMngr.GetComponent<AudioSource>().Play();
	}


}
