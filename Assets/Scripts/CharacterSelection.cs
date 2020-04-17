using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

	private GameObject[] characterList;
	private int index = 0;

	//UI
	public GameObject carSelect;
	public GameObject levelSelect;
	public AudioClip menuTap;
	public AudioClip carStart;
	public GameObject lockedImg;
	public GameObject unlockBtn;
	public GameObject confirmBtn;
	public Text unlockCostTxt;
	public Text unlockTitleTxt;
	public GameObject necScreen;
	public GameObject necText;
	public Button nextBtn;
	public Button previousBtn;
	public GameObject playBtn;
	public Text carTitle;
	public Text carDescr;

	MenuManager menuManager;
	UnityAdRewardCoins adManager;


	// Use this for initialization
	private void Start () 
	{
		menuManager = FindObjectOfType<MenuManager> ();
		adManager = FindObjectOfType<UnityAdRewardCoins> ();

		levelSelect.SetActive (false);
		lockedImg.SetActive (false);
		unlockBtn.SetActive (false);
		playBtn.SetActive (false);
		necScreen.SetActive (false);
		necText.GetComponent<Text> ().text = "Watch an ad to earn extra " + adManager.bonusCoins + " coins!";
		
		characterList = new GameObject[transform.childCount];

		//fill the array
		for (int i = 0; i < transform.childCount; i++) 
		{
			characterList [i] = transform.GetChild (i).gameObject;
		}
		//disable the renderer
		foreach (GameObject go in characterList) 
			go.SetActive (false);


		//enale index 0
		if (characterList [0])
			characterList [0].SetActive (true);

		CheckCharacterState ();
	}

	void Update()
	{
		carTitle.text = characterList [index].GetComponent<CharacterLock> ().carNameDisplay;
		carDescr.text = characterList [index].GetComponent<CharacterLock> ().carDescrDisplay;
	}

	public void ToggleLeft()
	{
		playSfx (menuTap);

		//disable current char
		characterList[index].SetActive(false);

		index--;
		if (index < 0)
			index = characterList.Length - 1;

		//enable next
		characterList[index].SetActive(true);

		CheckCharacterState ();

	}

	public void ToggleRight()
	{
		playSfx (menuTap);
		characterList[index].SetActive(false);

		index++;
		if (index == characterList.Length)
			index = 0;

		//enable next
		characterList[index].SetActive(true);

		CheckCharacterState ();
	}

	public void CheckCharacterState()
	{
		Debug.Log (characterList [index]);
		if (characterList [index].GetComponent<CharacterLock> ().isLocked == true) {
			unlockTitleTxt.text = "Locked:";
			lockedImg.SetActive (true);
			unlockBtn.SetActive (true);
			confirmBtn.SetActive (false);
			unlockCostTxt.text = "Unlock for " + characterList [index].GetComponent<CharacterLock> ().unlockCost.ToString () + " coins!";
			playBtn.SetActive (false);
		}
		else {
			unlockTitleTxt.text = "Ready:";
			lockedImg.SetActive (false);
			unlockBtn.SetActive (false);
			confirmBtn.SetActive (true);
			unlockCostTxt.text = " ";
			if (confirmBtn.activeInHierarchy == true)
				playBtn.SetActive (false);
		}
	}

	public void UnlockCharacter()
	{

		if (PlayerPrefs.GetInt ("Coins") == 0) {
			playSfx (menuTap);
			necScreen.SetActive (true);
			return;
		}
		if (PlayerPrefs.GetInt ("Coins") < characterList [index].GetComponent<CharacterLock> ().unlockCost) {
			playSfx (menuTap);
			necScreen.SetActive (true);
			return;
		}

		characterList[index].GetComponent<AudioSource>().Play();
		menuManager.userCoins -= characterList [index].GetComponent<CharacterLock> ().unlockCost;
		PlayerPrefs.SetInt ("Coins", menuManager.userCoins);
		menuManager.coinsTxt.text = PlayerPrefs.GetInt ("Coins").ToString();
		characterList [index].GetComponent<CharacterLock> ().isLocked = false;

		PlayerPrefsX.SetBool("CharacterUnlocked" + characterList [index].GetComponent<CharacterLock> ().characterName,
														characterList [index].GetComponent<CharacterLock> ().isLocked);

		lockedImg.SetActive (false);
		unlockBtn.SetActive (false);
		confirmBtn.SetActive (true);

		PlayerPrefs.Save ();

		CheckCharacterState ();
	}

	public void Confirm()
	{
		playSfx (menuTap);

		confirmBtn.SetActive(false);
		unlockBtn.SetActive (false);
		PlayerPrefs.SetInt ("Character", index);
		playBtn.SetActive (true);
	}

	public void PlayGame()
	{
		levelSelect.SetActive(true);
		playSfx (carStart);
	}

	void playSfx(AudioClip _sfx) {
		GetComponent<AudioSource>().clip = _sfx;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}
}
