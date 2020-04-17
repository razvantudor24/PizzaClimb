using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevels : MonoBehaviour {

	private GameObject[] levelsList;
	private int index = 0;

	public GameObject unlockPrompt;
	public Text unlockPromptDesc;
	public Text levelTitle;
	public Text levelDescription;
	public GameObject unlockCostInfo;
	public GameObject deliverTxt;
	public GameObject necStars;
	public GameObject necText;

	public AudioClip menuTap;

	MenuManager menuManager;
	UnityAdRewardCoins adManager;

	// Use this for initialization
	void Start () {

		menuManager = FindObjectOfType<MenuManager> ();
		adManager = FindObjectOfType<UnityAdRewardCoins> ();
		unlockPrompt.SetActive (false);
		necStars.SetActive (false);
		necText.GetComponent<Text> ().text = "Watch an ad to earn extra " + adManager.bonusStars + " stars!";

		levelsList = new GameObject [transform.childCount];

		for (int i = 0; i < transform.childCount; i++)
			{
			levelsList [i] = transform.GetChild (i).gameObject;
			}

		foreach (GameObject go in levelsList) 
			go.SetActive (false);

		//enale index 0
		if (levelsList [0])
			levelsList [0].SetActive (true);

		CheckLevelState ();
	}

	public void ToggleRight()
	{
		playSfx (menuTap);

		//disable current char
		levelsList[index].SetActive(false);

		index++;
		if (index == levelsList.Length)
			index = 0;


		//enable next
		levelsList[index].SetActive(true);

		CheckLevelState ();
	}

	public void ToggleLeft()
	{
		playSfx (menuTap);

		//disable current char
		levelsList[index].SetActive(false);

		index--;
		if (index < 0)
			index = levelsList.Length - 1;

		//enable next
		levelsList[index].SetActive(true);

		CheckLevelState ();
	}

	public void CheckLevelState()
	{
		Debug.Log (levelsList [index]);
		if (levelsList [index].GetComponent<LevelLock> ().isLocked == true) {
			levelsList [index].GetComponent<LevelLock> ().unlockBtn.SetActive (true);
			unlockCostInfo.SetActive (true);
			unlockCostInfo.GetComponent<Text>().text = "Unlock for " + levelsList [index].GetComponent<LevelLock> ().unlockCost.ToString() + " stars!";
			deliverTxt.GetComponent<Text> ().text = "Unlock!";

		} else {
			levelsList [index].GetComponent<LevelLock> ().unlockBtn.SetActive (false);
			deliverTxt.SetActive (true);
			deliverTxt.GetComponent<Text> ().text = "Deliver!";
			unlockCostInfo.SetActive (false);

		}
	}


	public void UnlockPromptShow()
	{
		playSfx (menuTap);
		unlockPrompt.SetActive (true);
		levelsList [index].GetComponent<Button> ().interactable = false;
		unlockPromptDesc.text = "Unlocking this level will cost you " + levelsList [index].GetComponent<LevelLock> ().unlockCost + " stars. Tap the button below to unlock!!!";
	}

	public void UnlockPromptClose()
	{
		playSfx (menuTap);
		unlockPrompt.SetActive (false);
		levelsList [index].GetComponent<Button> ().interactable = true;
	}


	public void UnlockLevel()
	{
		
		
		if (PlayerPrefs.GetInt ("Stars") == 0) {
			Debug.Log ("nec");
			playSfx (menuTap);
			necStars.SetActive (true);
			return;
		}
		if (PlayerPrefs.GetInt ("Stars") < levelsList [index].GetComponent<LevelLock> ().unlockCost) {
			Debug.Log ("nec");
			playSfx (menuTap);
			necStars.SetActive (true);
			return;
		}

		playSfx (menuTap);
		menuManager.userStars -= levelsList [index].GetComponent<LevelLock> ().unlockCost;
		PlayerPrefs.SetInt ("Stars", menuManager.userStars);
		menuManager.starsTxt.text = PlayerPrefs.GetInt ("Stars").ToString();
		levelsList [index].GetComponent<LevelLock> ().isLocked = false;

		PlayerPrefsX.SetBool("LevelUnlocked" + levelsList [index].GetComponent<LevelLock> ().levelNr,
			levelsList [index].GetComponent<LevelLock> ().isLocked);

		levelsList [index].GetComponent<LevelLock> ().unlockBtn.SetActive (false);
		deliverTxt.SetActive(true);
		unlockCostInfo.SetActive (false);

		UnlockPromptClose();

		PlayerPrefs.Save ();

		CheckLevelState ();

	}

	void playSfx(AudioClip _sfx) {
		GetComponent<AudioSource>().clip = _sfx;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}
	// Update is called once per frame
	void Update () {

		levelTitle.text = levelsList [index].GetComponent<LevelLock> ().levelName;
		levelDescription.text = levelsList [index].GetComponent<LevelLock> ().levelDescr;
	}
}
